﻿using System.Collections;
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
    public TextMeshPro gearStatus;

    public Gradient gradient;
    public SpriteRenderer rl, rr, fl, fr;

    // Start is called before the first frame update
    void Start()
    {
        bestTime = 9999999999999999999;
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

        if (control.automatic)
        {
            gearStatus.text = "AUTO";
        }
        else
        {
            gearStatus.text = "MANUAL";
        }

        if (enable)
        {
            if (GetComponent<TLimitControl>().goodTime)
            {
                timeText.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, msec);
            }
            else
            {
                timeText.text = string.Format("{0:00}:{1:00}:{2:00}" + "*", min, sec, msec);
            }
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
                if (GetComponent<TLimitControl>().goodTime)
                {
                    CompareTime(time);
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
    float time;
    float msec;
    float sec;
    float min;

    float bestTime;

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
        yield return new WaitForSeconds(4f);
        enable = true;
    }

    public void CompareTime(float t)
    {
        if (t < bestTime)
        {
            float newMsec = msec - 2f;
            bestTimeText.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, newMsec);

            bestTime = t;
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

    void TIREDISPLAY(SpriteRenderer rend, WheelFrictionCurve wfc, float init)
    {
        rend.color = gradient.Evaluate(wfc.extremumSlip / 2);
    }
}

