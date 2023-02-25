using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TLimitControl : MonoBehaviour
{
    public bool goodTime = true;
    public WheelCollider[] wheelColl;
    public WheelCollider[] wheelCollOffTrack;
    public bool ready = false;
    public AudioSource badTimeSound;
    DashControl dc;

    // Start is called before the first frame update
    void Start()
    {
        dc = GetComponent<DashControl>();
    }

    // Update is called once per frame
    void Update()
    {
        WheelHit hit;
        for (int i = 0; i < wheelColl.Length; i++)
        {
            if (wheelColl[i].GetGroundHit(out hit))
            {
                if (!hit.collider.gameObject.CompareTag("tracklimits") && !hit.collider.gameObject.CompareTag("rumblestrips"))
                {
                    wheelCollOffTrack[i] = wheelColl[i];
                }
                else
                {
                    wheelCollOffTrack[i] = null;
                }
            }
        }

        if (wheelCollOffTrack[0] != null && wheelCollOffTrack[1] != null && wheelCollOffTrack[2] != null && wheelCollOffTrack[3] != null)
        {
            if (goodTime)
            {
                badTimeSound.Play();
                goodTime = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "timer" && !ready)
        {
            ready = true;

            goodTime = true;
        }
        if (other.name == "timer2" && ready)
        {
            ready = false;
        }
    }
}
