using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DashControl : MonoBehaviour
{
    public CarControl control;
    public AudioControl ac;

    public GameObject[] REV;
    public Material[] revLight;

    public TextMeshPro speed;
    public TextMeshPro gear;
    public TextMeshPro lapCounter;

    public TimeControl tc;

    public bool lastLapGood = true;

    // Start is called before the first frame update
    void Start()
    {
        bestTime = 9999999999999999999;

        lapCounter.text = "LAP 0";
    }

    // Update is called once per frame
    void Update()
    {
        speed.text = control.speed.ToString("000") + " MPH";

        if (control.gear != 0)
        {
            gear.text = control.gear.ToString();
        }
        else
        {
            gear.text = "R";
        }

        lapCounter.text = "LAP " + tc.lapsUsed.ToString();

        if (tc.valid)
        {
            if (enable)
            {
                if (GetComponent<TLimitControl>().goodTime)
                {
                    timeText.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, msec);
                }
                else
                {
                    timeText.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, msec);
                }
            }
        }
        else
        {
            timeText.text = "00:00:00";
        }

        REVLED();
    }

    public TextMeshPro timeText;
    public TextMeshPro bestTimeText;
    public bool ready = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "timer" && !ready)
        {
            ready = true;

            if (!initialized)
            {
                StartCoroutine(StopWatch());
                initialized = true;
            }
            else
            {
                if (tc.valid)
                {
                    float newTime = time - 0.02f;
                    if (GetComponent<TLimitControl>().goodTime)
                    {
                        CompareTime(newTime);
                    }

                    tc.AddTimeString(newTime);
                    tc.lapNum.Add(tc.lapsUsed.ToString());
                    if (GetComponent<TLimitControl>().goodTime)
                    {
                        tc.validated.Add("Y");
                    }
                    else
                    {
                        tc.validated.Add("N");
                    }
                    tc.lapsUsed++;
                }

                StartCoroutine(Disable());
            }
        }
        if (other.name == "timer2" && ready)
        {
            ready = false;
        }
    }

    public bool initialized = false;
    public bool stopWatchOn = false;
    public float time;
    float msec;
    float sec;
    float min;

    public float bestTime;

    public IEnumerator StopWatch()
    {
        stopWatchOn = true;
        while (true)
        {
            time += Time.deltaTime;
            msec = (int)((time - (int)time) * 100);
            sec = (int)(time % 60);
            min = (int)(time / 60 % 60);

            yield return null;
        }
    }

    public bool enable = true;
    public IEnumerator Disable()
    {
        enable = false;
        time = 0;
        msec = 0;
        sec = 0;
        min = 0;
        lastLapGood = GetComponent<TLimitControl>().goodTime;
        yield return new WaitForSeconds(4f);
        enable = true;
    }

    public GhostRecorder gr;
    public void CompareTime(float t)
    {
        if (t < bestTime)
        {
            float newT = t;
            bestTime = newT;

            float msec_best = (int)((newT - (int)newT) * 100);
            float sec_best = (int)(newT % 60);
            float min_best = (int)(newT / 60 % 60);
            string bestTimeString = string.Format("{0:00}:{1:00}:{2:00}", min_best, sec_best, msec_best);
            bestTimeText.text = bestTimeString;

            HUDControl hc = FindObjectOfType<HUDControl>();
            if (hc != null)
            {
                StartCoroutine(hc.ChangeToPurple());
            }
        }
    }

    private void REVLED()
    {
        for (int i = 0; i < REV.Length; i++)
        {
            if (i <= 5)
            {
                REV1(REV[i], i, 0.925f + .009f * (i + 1));
            }
            if (i > 5 && i <= 9)
            {
                REV2(i);
            }
        }
    }

    void REV1(GameObject rpmLight, int index, float rpmVal)
    {
        if (ac.engine.pitch >= rpmVal)
        {
            if (index >= 0 && index <= 4)
            {
                rpmLight.GetComponent<Renderer>().sharedMaterial = revLight[1];
                foreach (Transform child in rpmLight.transform)
                {
                    child.GetComponent<Light>().enabled = true;
                    if (child.name == "color")
                    {
                        child.GetComponent<Light>().color = rpmLight.GetComponent<Renderer>().sharedMaterial.color;
                    }
                    else
                    {
                        child.GetComponent<Light>().color = Color.white;
                    }
                }
            }
            if (index == 5)
            {
                rpmLight.GetComponent<Renderer>().sharedMaterial = revLight[2];
                foreach (Transform child in rpmLight.transform)
                {
                    child.GetComponent<Light>().enabled = true;
                    if (child.name == "color")
                    {
                        child.GetComponent<Light>().color = rpmLight.GetComponent<Renderer>().sharedMaterial.color;
                    }
                    else
                    {
                        child.GetComponent<Light>().color = Color.white;
                    }
                }
            }
        }
        else
        {
            rpmLight.GetComponent<Renderer>().sharedMaterial = revLight[0];
            foreach (Transform child in rpmLight.transform)
            {
                child.GetComponent<Light>().enabled = false;
            }
        }
    }

    void REV2(int i)
    {
        if (REV[5].GetComponent<Renderer>().sharedMaterial == revLight[2])
        {
            REV[i].GetComponent<Renderer>().sharedMaterial = revLight[2];
            foreach (Transform child in REV[i].transform)
            {
                child.GetComponent<Light>().enabled = true;
                if (child.name == "color")
                {
                    child.GetComponent<Light>().color = REV[i].GetComponent<Renderer>().sharedMaterial.color;
                }
                else
                {
                    child.GetComponent<Light>().color = Color.white;
                }
            }
        }
        else
        {
            REV[i].GetComponent<Renderer>().sharedMaterial = revLight[0];
            foreach (Transform child in REV[i].transform)
            {
                child.GetComponent<Light>().enabled = false;
            }
        }
    }
}

