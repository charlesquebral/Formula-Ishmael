using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DRSControl : MonoBehaviour
{
    public GameObject wingFlap;
    public GameObject bulb;
    public Material[] indLight;
    [SerializeField] Vector3[] wingRot;
    public bool drs = false;
    public bool status = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "DRSt")
        {

        }
        if (other.name == "DRSf")
        {
            drs = false;
            GetComponent<CarControl>().motorForce = 600;
            GetComponent<CarControl>().limiter = 0;
            status = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && drs && !GetComponent<CarControl>().automatic)
        {
            status = !status;

            if (status)
            {
                GetComponent<CarControl>().motorForce = 660;
                GetComponent<CarControl>().limiter = 0.3f;
            }
            else
            {
                GetComponent<CarControl>().motorForce = 600;
                GetComponent<CarControl>().limiter = 0;
            }
        }
        else if (drs && GetComponent<CarControl>().automatic)
        {
            status = true;
            GetComponent<CarControl>().motorForce = 660;
            GetComponent<CarControl>().limiter = 0.3f;
        }

        if (GetComponent<InputManager>())
        {
            if (GetComponent<InputManager>().vertical < 0)
            {
                status = false;
            }
        }

        if (status)
        {
            wingFlap.transform.localRotation = Quaternion.Slerp(wingFlap.transform.localRotation, Quaternion.Euler(wingRot[1]), 15f * Time.deltaTime);
        }
        else
        {
            wingFlap.transform.localRotation = Quaternion.Slerp(wingFlap.transform.localRotation, Quaternion.Euler(wingRot[0]), 15f * Time.deltaTime);
        }

        if (bulb != null)
        {
            DoLights();
        }
    }

    private void DoLights()
    {
        if (!drs && !status)
        {
            bulb.GetComponent<MeshRenderer>().material = indLight[0];
            foreach (Transform child in bulb.transform)
            {
                child.GetComponent<Light>().enabled = false;
            }
        }
        else if (drs && !status)
        {
            bulb.GetComponent<MeshRenderer>().material = indLight[1];
            foreach (Transform child in bulb.transform)
            {
                child.GetComponent<Light>().enabled = true;
                if (child.name == "color")
                {
                    child.GetComponent<Light>().color = bulb.GetComponent<Renderer>().sharedMaterial.color;
                }
                else
                {
                    child.GetComponent<Light>().color = Color.white;
                }
            }
        }
        else
        {
            bulb.GetComponent<MeshRenderer>().material = indLight[2];
            foreach (Transform child in bulb.transform)
            {
                child.GetComponent<Light>().enabled = true;
                if (child.name == "color")
                {
                    child.GetComponent<Light>().color = bulb.GetComponent<Renderer>().sharedMaterial.color;
                }
                else
                {
                    child.GetComponent<Light>().color = Color.white;
                }
            }
        }
    }

    public AudioClip beep;
    public AudioSource beepSource;
    public void PlaySound()
    {
        beepSource.PlayOneShot(beep, 0.5f);
    }
}
