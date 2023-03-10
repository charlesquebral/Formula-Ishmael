using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaHolder : MonoBehaviour
{
    public float bestTimeHere;
    public float currTimeHere;

    public float deltComp;
    public float delta;

    public TimeControl tc;
    public DashControl dc;
    public HUDControl hc;

    public float delt;

    float msec;
    float sec;
    float min;

    public string deltaText;

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.name == "fw")
        {
            currTimeHere = dc.time;

            if (tc.valid && tc.lapsUsed >= 2)
            {
                if (bestTimeHere != 0)
                {
                    deltComp = dc.time - bestTimeHere;
                    delt = Mathf.Abs(dc.time - bestTimeHere);

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

                    hc.delta.text = deltaText;
                }
            }
        }
    }*/
}
