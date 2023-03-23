using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaTimeControl : MonoBehaviour
{
    public TimeControl tc;
    public DashControl dc;
    public HUDControl hc;

    public float deltComp;
    public float delta;
    public bool ready = false;

    public bool initialized = false;

    float msec;
    float sec;
    float min;

    public string deltaText;

    public float delt;

    public DeltaHolder[] beams;
    public Transform closestBeam;

    private void Start()
    {
        //beams = FindObjectsOfType<DeltaHolder>();

        //closestBeam = GetClosestBeam();
    }

    /*private void Update()
    {
        closestBeam = GetClosestBeam();

        if (closestBeam != null)
        {
            closestBeam.gameObject.GetComponent<DeltaHolder>().currTimeHere = dc.time;

            if (closestBeam.gameObject.GetComponent<DeltaHolder>().bestTimeHere != 0)
            {
                deltComp = dc.time - closestBeam.gameObject.GetComponent<DeltaHolder>().bestTimeHere;
                delt = Mathf.Abs(dc.time - closestBeam.gameObject.GetComponent<DeltaHolder>().bestTimeHere);

                msec = (int)((delta - (int)delta) * 100);
                sec = (int)(delta % 60);
                min = (int)(delta / 60 % 60);

                if (deltComp > 0)
                {
                    //string comp = "+";
                    //string compd = string.Format("{0:00}:{1:00}:{2:00}", min, sec, msec);
                    //deltaText = comp + compd;
                    deltaText = delt.ToString("+0.00");
                }
                else if (deltComp < 0)
                {
                    //string comp = "-";
                    //string compd = string.Format("{0:00}:{1:00}:{2:00}", min, sec, msec);
                    //deltaText = comp + compd;
                    deltaText = delt.ToString("-0.00");
                }
                else
                {
                    deltaText = "+/-0.00";
                }
            }
        }
    }

    public IEnumerator CompareTime()
    {
        yield return new WaitForSeconds(0.25f);

        hc.delta.text = deltaText;

        StartCoroutine(CompareTime());
    }

    public Transform GetClosestBeam()
    {

        float closestDistance = Mathf.Infinity;
        Transform trans = null;
        foreach (DeltaHolder go in beams)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, go.transform.position);

            if (currentDistance <= closestDistance)
            {
                closestDistance = currentDistance;
                trans = go.transform;
            }
        }
        return trans;
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "timer" && !ready)
        {
            ready = true;

            if (initialized)
            {
                if (dc.lastLapGood)
                {
                    //StartCoroutine(CompareTime());

                    if ((dc.time - 0.02f) < dc.bestTime)
                    {
                        DeltaHolder[] dh = FindObjectsOfType<DeltaHolder>();
                        for (int i = 0; i < dh.Length; i++)
                        {
                            dh[i].bestTimeHere = dh[i].currTimeHere;
                        }
                    }
                }
            }
            else
            {
                initialized = true;
            }
        }
        if (other.name == "timer2" && ready)
        {
            ready = false;
        }

        if (other.name == "beam")
        {
            other.gameObject.GetComponent<DeltaHolder>().currTimeHere = dc.time;

            if (tc.valid && tc.lapsUsed >= 2)
            {
                if (other.gameObject.GetComponent<DeltaHolder>().bestTimeHere != 0)
                {
                    deltComp = dc.time - other.gameObject.GetComponent<DeltaHolder>().bestTimeHere;
                    delt = Mathf.Abs(dc.time - other.gameObject.GetComponent<DeltaHolder>().bestTimeHere);

                    msec = (int)((delta - (int)delta) * 100);
                    sec = (int)(delta % 60);
                    min = (int)(delta / 60 % 60);

                    if (deltComp > 0)
                    {
                        //string comp = "+";
                        //string compd = string.Format("{0:00}:{1:00}:{2:00}", min, sec, msec);
                        //deltaText = comp + compd;
                        deltaText = delt.ToString("+0.00");
                    }
                    else if (deltComp < 0)
                    {
                        //string comp = "-";
                        //string compd = string.Format("{0:00}:{1:00}:{2:00}", min, sec, msec);
                        //deltaText = comp + compd;
                        deltaText = delt.ToString("-0.00");
                    }
                    else
                    {
                        deltaText = string.Format("{0:00}:{1:00}:{2:00}", min, sec, msec);
                    }
                }
            }
        }
    }
}
