using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataGather : MonoBehaviour
{
    public bool initialized = false;

    public CarControl cc;
    public DashControl dc;
    public TimeControl tc;
    public TLimitControl tlc;

    public List<string> avgSpeed = new List<string>();
    public List<string> distTraveled = new List<string>();

    private List<float> speeds = new List<float>();
    private List<float> dists = new List<float>();

    public bool ready = false;

    public List<string> timeThrot = new List<string>();
    public List<string> timeBrake = new List<string>();
    public List<string> timeCoast = new List<string>();

    public bool stopWatchOn = false;
    float timeT, msecT, secT, minT;
    float timeB, msecB, secB, minB;
    float timeC, msecC, secC, minC;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "timer" && !ready)
        {
            StartCoroutine(Record());

            ready = true;

            if (initialized)
            {
                if (speeds.Count > 0)
                {
                    float mph = speeds.Average() * 2.237f;
                    float kph = speeds.Average() * 3.6f;
                    string avgSpeedString = mph.ToString("000.000") + " / " + kph.ToString("000.000");
                    avgSpeed.Add(avgSpeedString);
                }

                if (dists.Count > 0)
                {
                    float mi = 0;
                    for (int i = 0; i < dists.Count; i++)
                    {
                        mi += dists[i] * 0.000621371f;
                    }
                    float km = 0;
                    for (int i = 0; i < dists.Count; i++)
                    {
                        km += dists[i] * 0.001f;
                    }
                    string avgDistString = mi.ToString("000.000") + " / " + km.ToString("000.000");
                    distTraveled.Add(avgDistString);
                }

                string timeTString = string.Format("{0:00}:{1:00}:{2:00}", minT, secT, msecT);
                timeThrot.Add(timeTString);
                string timeBString = string.Format("{0:00}:{1:00}:{2:00}", minB, secB, msecB);
                timeBrake.Add(timeBString);
                string timeCString = string.Format("{0:00}:{1:00}:{2:00}", minC, secC, msecC);
                timeCoast.Add(timeCString);

                speeds.Clear();
                dists.Clear();

                Clear();
            }
            else
            {
                StartCoroutine(StopWatch());

                lastPos = transform.position;

                initialized = true;
            }
        }
        if (other.name == "timer2" && ready)
        {
            ready = false;
        }
    }

    private Vector3 lastPos;
    private Vector3 currPos;
    private IEnumerator Record()
    {
        if (tc.valid && initialized)
        {
            if (cc.rb.velocity.magnitude > 0.1f)
            {
                speeds.Add(cc.rb.velocity.magnitude);
            }

            float distToAdd;
            currPos = transform.position;
            if (lastPos != currPos)
            {
                distToAdd = Vector3.Distance(lastPos, currPos);

                dists.Add(distToAdd);
            }
            lastPos = currPos;
        }
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(Record());
    }

    public IEnumerator StopWatch()
    {
        stopWatchOn = true;
        while (true)
        {
            if (cc.GetComponent<InputManager>().vertical > 0)
            {
                timeT += Time.deltaTime;
                msecT = (int)((timeT - (int)timeT) * 100);
                secT = (int)(timeT % 60);
                minT = (int)(timeT / 60 % 60);
            }
            else if (cc.GetComponent<InputManager>().vertical < 0)
            {
                timeB += Time.deltaTime;
                msecB = (int)((timeB - (int)timeB) * 100);
                secB = (int)(timeB % 60);
                minB = (int)(timeB / 60 % 60);
            }
            else
            {
                timeC += Time.deltaTime;
                msecC = (int)((timeC - (int)timeC) * 100);
                secC = (int)(timeC % 60);
                minC = (int)(timeC / 60 % 60);
            }

            yield return null;
        }
    }

    public void Clear()
    {
        timeT = 0;
        msecT = 0;
        secT = 0;
        minT = 0;
        timeB = 0;
        msecB = 0;
        secB = 0;
        minB = 0;
        timeC = 0;
        msecC = 0;
        secC = 0;
        minC = 0;
    }
}
