using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    public int lapsAvailable = 5;
    public int lapsUsed = 0;

    public bool valid = true;

    public List<string> lapNum = new List<string>();
    public List<string> times = new List<string>();
    public List<string> validated = new List<string>();

    private void Start()
    {
        
    }

    private void Update()
    {
        if (valid)
        {
            if (lapsUsed > lapsAvailable)
            {
                valid = false;

                lapsUsed = lapsAvailable;
            }
        }
        else
        {
            CarControl cc = FindObjectOfType<CarControl>();
            if (cc != null)
            {
                cc.stopped = true;
            }
        }
    }

    public void AddTimeString(float t)
    {
        float msec = (int)((t - (int)t) * 100);
        float sec = (int)(t % 60);
        float min = (int)(t / 60 % 60);

        string time = string.Format("{0:00}:{1:00}:{2:00}", min, sec, msec);
        times.Add(time);
    }
}
