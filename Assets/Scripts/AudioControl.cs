using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    public CarControl cc;
    public AudioSource engine;
    public AudioSource pop;
    public AudioSource kerb;
    public float pitch;
    public float[] startingPitch;
    public float[] pitchDiv;
    public float popTime;
    int last_gear = 0;
    private List<WheelCollider> wC;
    private WheelCollider[] coll;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CarControl>();
        engine.pitch = 0.5f;
        StartCoroutine(BlownDiffuser());

        coll = new WheelCollider[4];
        coll[0] = cc.RL;
        coll[1] = cc.RR;
        coll[2] = cc.FL;
        coll[3] = cc.FR;

        wC = new List<WheelCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (engine.pitch >= 0.5 && engine.pitch <= 1.035)
        {
            if (cc.gear == 0)
            {
                engine.pitch = 0.5f;
            }
            else
            {
                if (cc.speed > 0.1f)
                {
                    engine.pitch = startingPitch[cc.gear] + (cc.speed / cc.topFWDSpeeds[cc.gear] * pitchDiv[cc.gear]);
                }
                else
                {
                    engine.pitch = 0.5f;
                }
            }
        }
        if (engine.pitch < 0.5)
        {
            engine.pitch = 0.5f;
        }
        if (engine.pitch > 1.035)
        {
            engine.pitch = 1.035f;
        }

        if (cc.verticalInput < 1 && popTime > 0.045f)
        {
            popTime -= .001f;
        }

        if (last_gear != cc.gear)
        {
            if (pop != null)
            {
                pop.Play();
                popTime = 0.073f;
            }
            last_gear = cc.gear;
        }

        KerbSound();
    }

    void KerbSound()
    {
        WheelHit hit;

        for (int i = 0; i < coll.Length; i++)
        {
            if (coll[i].GetGroundHit(out hit))
            {
                if (hit.collider.gameObject.CompareTag("rumblestrips"))
                {
                    if (!wC.Contains(coll[i]))
                    {
                        wC.Add(coll[i]);
                    }
                }
                else
                {
                    if (wC.Contains(coll[i]))
                    {
                        wC.Remove(coll[i]);
                    }
                }
            }
            else
            {
                if (wC.Contains(coll[i]))
                {
                    wC.Remove(coll[i]);
                }
            }
        }

        if (wC.Count > 0 && cc.speed > 1f)
        {
            kerb.enabled = true;
        }
        else
        {
            kerb.enabled = false;
        }

        if (kerb.enabled)
        {
            kerb.pitch = 0.65f + cc.speed * 0.0005f;

            if (kerb.pitch > 0.9f)
            {
                kerb.pitch = 0.9f;
            }
            else if (kerb.pitch < 0.65f)
            {
                kerb.pitch = 0.65f;
            }
        }
    }

    IEnumerator BlownDiffuser()
    {
        if (pop != null)
        {
            if (cc.verticalInput < 1 && cc.speed > 10f && cc.gear != 0)
            {
                //pop.Play();
            }
        }
        yield return new WaitForSeconds(popTime);
        StartCoroutine(BlownDiffuser());
    }
}
