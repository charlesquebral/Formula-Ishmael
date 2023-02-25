using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerAnim : MonoBehaviour
{
    public GameObject stem;
    [SerializeField] Vector3[] myAngles;
    [SerializeField] [Range(0f, 10f)] float lerpTime;
    public InputManager im;
    public CarControl cc;

    [Header("Left Arm")]
    public Transform shoulderL;
    public Transform forearmL;
    public Transform handL;
    [SerializeField] Vector3[] armoneLeft;
    [SerializeField] Vector3[] armTwoLeft;
    [SerializeField] Vector3[] handLeft;

    [Header("Right Arm")]
    public Transform shoulderR;
    public Transform forearmR;
    public Transform handR;
    [SerializeField] Vector3[] armoneRight;
    [SerializeField] Vector3[] armTwoRight;
    [SerializeField] Vector3[] handRight;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CarControl>();
    }

    //+ = left, - = right

    // Update is called once per frame
    void FixedUpdate()
    {
        if (im.horizontal > 0)
        {
            stem.transform.localRotation = Quaternion.Slerp(stem.transform.localRotation, Quaternion.Euler(myAngles[1]), lerpTime * Time.deltaTime);

            //shoulderL.transform.localRotation = Quaternion.Slerp(shoulderL.transform.localRotation, Quaternion.Euler(armoneLeft[2]), lerpTime * Time.deltaTime);
            //forearmL.transform.localRotation = Quaternion.Slerp(forearmL.transform.localRotation, Quaternion.Euler(armTwoLeft[2]), lerpTime * Time.deltaTime);
            //handL.transform.localRotation = Quaternion.Slerp(handL.transform.localRotation, Quaternion.Euler(handLeft[2]), lerpTime * Time.deltaTime);

            //shoulderR.transform.localRotation = Quaternion.Slerp(shoulderR.transform.localRotation, Quaternion.Euler(armoneRight[2]), lerpTime * Time.deltaTime);
            //forearmR.transform.localRotation = Quaternion.Slerp(forearmR.transform.localRotation, Quaternion.Euler(armTwoRight[2]), lerpTime * Time.deltaTime);
            //handR.transform.localRotation = Quaternion.Slerp(handR.transform.localRotation, Quaternion.Euler(handRight[2]), lerpTime * Time.deltaTime);
        }
        else if (im.horizontal < 0)
        {
            stem.transform.localRotation = Quaternion.Slerp(stem.transform.localRotation, Quaternion.Euler(myAngles[2]), lerpTime * Time.deltaTime);

            //shoulderL.transform.localRotation = Quaternion.Slerp(shoulderL.transform.localRotation, Quaternion.Euler(armoneLeft[1]), lerpTime * Time.deltaTime);
            //forearmL.transform.localRotation = Quaternion.Slerp(forearmL.transform.localRotation, Quaternion.Euler(armTwoLeft[1]), lerpTime * Time.deltaTime);
            //handL.transform.localRotation = Quaternion.Slerp(handL.transform.localRotation, Quaternion.Euler(handLeft[1]), lerpTime * Time.deltaTime);

            //shoulderR.transform.localRotation = Quaternion.Slerp(shoulderR.transform.localRotation, Quaternion.Euler(armoneRight[1]), lerpTime * Time.deltaTime);
            //forearmR.transform.localRotation = Quaternion.Slerp(forearmR.transform.localRotation, Quaternion.Euler(armTwoRight[1]), lerpTime * Time.deltaTime);
            //handR.transform.localRotation = Quaternion.Slerp(handR.transform.localRotation, Quaternion.Euler(handRight[1]), lerpTime * Time.deltaTime);
        }
        else
        {
            stem.transform.localRotation = Quaternion.Slerp(stem.transform.localRotation, Quaternion.Euler(myAngles[0]), lerpTime * Time.deltaTime);

            //shoulderL.transform.localRotation = Quaternion.Slerp(shoulderL.transform.localRotation, Quaternion.Euler(armoneLeft[0]), lerpTime * Time.deltaTime);
            //forearmL.transform.localRotation = Quaternion.Slerp(forearmL.transform.localRotation, Quaternion.Euler(armTwoLeft[0]), lerpTime * Time.deltaTime);
            //handL.transform.localRotation = Quaternion.Slerp(handL.transform.localRotation, Quaternion.Euler(handLeft[0]), lerpTime * Time.deltaTime);

            //shoulderR.transform.localRotation = Quaternion.Slerp(shoulderR.transform.localRotation, Quaternion.Euler(armoneRight[0]), lerpTime * Time.deltaTime);
            //forearmR.transform.localRotation = Quaternion.Slerp(forearmR.transform.localRotation, Quaternion.Euler(armTwoRight[0]), lerpTime * Time.deltaTime);
            //handR.transform.localRotation = Quaternion.Slerp(handR.transform.localRotation, Quaternion.Euler(handRight[0]), lerpTime * Time.deltaTime);
        }
    }
}

