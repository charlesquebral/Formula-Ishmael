using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorCam : MonoBehaviour
{
    public Camera cam;

    public TimeControl tc;

    public GameObject gameTarget, postGameTarget;

    public Transform target;

    float lastDist;

    public float zoomSpeed;

    // Start is called before the first frame update
    void Start()
    {
        lastDist = Vector3.Distance(transform.position, target.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (tc.valid)
        {
            if (target != gameTarget)
            {
                target = gameTarget.transform;
            }
        }
        else
        {
            if (target != postGameTarget)
            {
                target = postGameTarget.transform;
            }
        }

        transform.LookAt(target.position);

        float currDist = Vector3.Distance(transform.position, target.position);

        if (currDist - lastDist >= 0.01f || currDist - lastDist <= -0.01f)
        {
            if (currDist < lastDist) //approaching
            {
                if (cam.fieldOfView > 15)
                {
                    cam.fieldOfView -= zoomSpeed;
                }
            }
            else if (currDist > lastDist) //leaving
            {
                if (cam.fieldOfView < 40)
                {
                    cam.fieldOfView += zoomSpeed;
                }
            }
        }

        lastDist = currDist;
    }
}
