using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera[] fronts; //0 pov, 1 cent, 2 offs
    public Camera[] rears; //0 pov, 1 cent, 2 offs

    public int cameraIndex = 0;

    public GameObject[] spectators;
    public GameObject closestSpectator;

    private void Start()
    {
        spectators = GameObject.FindGameObjectsWithTag("cams");

        closestCam = GetClosestCam();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            cameraIndex++;

            if (cameraIndex == 4) //4
            {
                cameraIndex = 0;
            }
        }

        if (cameraIndex == 3)
        {
            Spectate();
        }
        else
        {
            OnBoard();
        }
    }

    void OnBoard()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            for (int i = 0; i < rears.Length; i++)
            {
                if (i != cameraIndex)
                {
                    rears[i].enabled = false;
                    rears[i].GetComponent<AudioListener>().enabled = false;
                }
                else
                {
                    rears[i].enabled = true;
                    rears[i].GetComponent<AudioListener>().enabled = true;
                }
            }

            for (int i = 0; i < fronts.Length; i++)
            {
                fronts[i].enabled = false;
                fronts[i].GetComponent<AudioListener>().enabled = false;
            }
        }
        else
        {
            for (int i = 0; i < fronts.Length; i++)
            {
                if (i != cameraIndex)
                {
                    fronts[i].enabled = false;
                    fronts[i].GetComponent<AudioListener>().enabled = false;
                }
                else
                {
                    fronts[i].enabled = true;
                    fronts[i].GetComponent<AudioListener>().enabled = true;
                }
            }
            for (int i = 0; i < rears.Length; i++)
            {
                rears[i].enabled = false;
                rears[i].GetComponent<AudioListener>().enabled = false;
            }
        }

        spectators = GameObject.FindGameObjectsWithTag("cams");
        foreach (GameObject GO in spectators)
        {
            if (GO.name == "Spectator")
            {
                GO.GetComponent<Camera>().enabled = false;
                GO.GetComponent<AudioListener>().enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CameraTrigger>())
        {
            //print("ishmael");
            int i = Random.Range(0, other.GetComponent<CameraTrigger>().targetCam.Length);
            if (other.GetComponent<CameraTrigger>().targetCam[i].GetComponent<SpectatorCam>())
            {
                closestCam = other.GetComponent<CameraTrigger>().targetCam[i].GetComponent<SpectatorCam>().cam.transform;
            }
            else
            {
                closestCam = other.GetComponent<CameraTrigger>().targetCam[i].transform;
            }
        }
    }

    public float distanceToClosestSpectator = Mathf.Infinity;
    public Transform closestCam;

    void Spectate()
    {
        for (int i = 0; i < fronts.Length; i++)
        {
            fronts[i].enabled = false;
            fronts[i].GetComponent<AudioListener>().enabled = false;
        }
        for (int i = 0; i < rears.Length; i++)
        {
            rears[i].enabled = false;
            rears[i].GetComponent<AudioListener>().enabled = false;
        }

        for (int i = 0; i < spectators.Length; i++)
        {
            if (spectators[i].transform == closestCam)
            {
                spectators[i].GetComponent<Camera>().enabled = true;
                spectators[i].GetComponent<AudioListener>().enabled = true;
            }
            else
            {
                spectators[i].GetComponent<Camera>().enabled = false;
                spectators[i].GetComponent<AudioListener>().enabled = false;
            }
        }
    }

    public Transform GetClosestCam()
    {

        float closestDistance = Mathf.Infinity;
        Transform trans = null;
        foreach (GameObject go in spectators)
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
    }
}
