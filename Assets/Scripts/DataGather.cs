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
                    string avgSpeedString = mph.ToString("F2") + " / " + kph.ToString("F2");
                    avgSpeed.Add(avgSpeedString);
                }

                if (dists.Count > 0)
                {
                    float mi = dists.Average() * 0.000621371f;
                    float km = dists.Average() * 0.001f;
                    string avgDistString = mi.ToString("F5") + " / " + km.ToString("F5");
                    distTraveled.Add(avgDistString);
                }

                speeds.Clear();
                dists.Clear();
            }
            else
            {
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
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Record());
    }
}
