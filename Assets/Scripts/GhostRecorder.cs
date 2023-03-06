using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRecorder : MonoBehaviour
{
    public Ghost ghost;
    public GhostPlayer gp;
    public float timer;
    public float timerVal;

    public DashControl dc;

    // Start is called before the first frame update
    void Awake()
    {
        ghost.ResetData();
        ghost.ResetBestData();
        ghost.isRecord = false;
        ghost.isReplay = false;

        if (ghost.isRecord)
        {
            ghost.ResetData();
            timerVal = 0;
            timer = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ghost.isRecord)
        {
            timer += Time.unscaledDeltaTime;
            timerVal += Time.unscaledDeltaTime;

            if (ghost.isRecord && timer >= 1 / ghost.recordFrequency)
            {
                ghost.timeStampCurr.Add(timerVal);
                ghost.positionCurr.Add(this.transform.position);
                ghost.rotationCurr.Add(this.transform.eulerAngles);

                timer = 0;
            }
        }
    }

    public bool recordingStarted = false;
    public bool ready = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "timer" && ready)
        {
            ready = false;

            if (!recordingStarted)
            {
                recordingStarted = true;

                //start recording
                ghost.ResetData();
                ghost.isRecord = true;
            }
            else
            {
                //start replaying
                if (dc.lastLapGood)
                {
                    if ((dc.time - 0.02f) < dc.bestTime)
                    {
                        ghost.AssignNewData();
                    }
                }

                if (ghost.timeStamp.Count > 0)
                {
                    ghost.isReplay = true;
                }

                gp.timerVal = 0;
                ghost.ResetData();
            }

            timer = 0;
            timerVal = 0;
        }
        else if (other.name == "timer2" && !ready)
        {
            ready = true;
        }
    }
}
