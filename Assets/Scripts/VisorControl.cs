using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisorControl : MonoBehaviour
{
    public CameraController cc;

    public CarControl carc;

    public MeshRenderer visor;

    public GameObject visorObj;
    bool closed = true;
    [SerializeField] Vector3[] visRot;

    public Camera cam;

    public float starterFloat;

    // Start is called before the first frame update
    void Start()
    {
        carc = transform.root.GetComponent<CarControl>();

        if (visor != null)
        {
            starterFloat = visor.material.GetFloat("_Transparency");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cc != null)
        {
            if (cc.cameraIndex == 0)
            {
                visor.material.SetFloat("_Transparency", 0.85f);
            }
            else
            {
                visor.material.SetFloat("_Transparency", starterFloat);
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            closed = !closed;
        }

        if (closed)
        {
            visorObj.transform.localRotation = Quaternion.Slerp(visorObj.transform.localRotation, Quaternion.Euler(visRot[0]), 15f * Time.deltaTime);
        }
        else
        {
            visorObj.transform.localRotation = Quaternion.Slerp(visorObj.transform.localRotation, Quaternion.Euler(visRot[1]), 15f * Time.deltaTime);
        }
    }
}
