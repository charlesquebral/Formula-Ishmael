using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetFreeLook : MonoBehaviour
{
    public CarControl cc;
    public Transform cam;
    public Transform helmet;
    public bool lookAround = false;

    public float mouseSensitivity = 100f;
    public float clampRotx = 45f;
    public float clampRoty = 45f;
    float xRotation = 0f;
    float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (cc.automatic)
        {
            if (Input.GetMouseButtonDown(1))
            {
                lookAround = !lookAround;
            }

            if (lookAround)
            {
                MoveFreely();
            }
            else
            {
                ReturnToZero();
            }
        }
        else
        {
            ReturnToZero();
        }

        //helmet.transform.localRotation = cam.transform.localRotation;
    }

    void MoveFreely()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -clampRotx, clampRotx);
        yRotation = Mathf.Clamp(yRotation, -clampRoty, clampRoty);

        cam.transform.localRotation = Quaternion.Slerp(cam.transform.localRotation, Quaternion.Euler(xRotation, yRotation, 0f), 5f * Time.deltaTime);
    }

    void ReturnToZero()
    {
        cam.transform.transform.localRotation = Quaternion.Slerp(cam.transform.localRotation, Quaternion.Euler(Vector3.zero), 5f * Time.deltaTime);
    }
}
