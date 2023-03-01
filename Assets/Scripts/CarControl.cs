using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarControl : MonoBehaviour
{
    public InputManager im;

    public Vector3 _centerOfMass;
    public Rigidbody rb;

    public float horizontalInput;
    public float verticalInput;
    public float steeringAngle;

    public WheelCollider RR, RL;
    public WheelCollider FR, FL;

    public Transform RR_transform, RL_transform;
    public Transform FR_transform, FL_transform;

    public GameObject fr_aero, fl_aero;

    public float maxSteerAngle;
    public AnimationCurve steerCurve;
    public float brakeForce;

    public float motorForce = 50;
    public float revForce = 10;
    public float limiter = .5f;

    public float speed;

    public float[] topFWDSpeeds;
    public float topREVSpeed = 20;

    public int gear = 1;
    public int maxGear = 8;
    public bool automatic;

    public WheelFrictionCurve wfc;

    public bool stopped = false;

    private void Update()
    {
        if (automatic)
        {
            AutoGears();
        }
        else
        {
            ManualGears();
        }
        HandleGearMode();

        if (!stopped)
        {
            if (RR != null && RL != null)
            {
                HandleBurnOut();
            }
        }
    }

    private void FixedUpdate()
    {
        rb.centerOfMass = _centerOfMass;
        speed = rb.velocity.magnitude * 2.237f;

        if (!stopped)
        {
            GetInput();
            if (RR != null && RL != null && FR != null && FL != null)
            {
                if (automatic)
                {
                    AccelerateAuto();
                }
                else
                {
                    AccelerateManual();
                }
                Steer();
            }
        }
        else
        {
            RR.brakeTorque = brakeForce;
            RL.brakeTorque = brakeForce;
            FR.brakeTorque = brakeForce;
            FL.brakeTorque = brakeForce;
        }
        UpdateWheelPoses();
    }

    private void HandleGearMode()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            automatic = !automatic;
        }
    }

    private void HandleBurnOut()
    {
        wfc.extremumValue = 1;
        wfc.asymptoteSlip = 0.5f;
        wfc.asymptoteValue = .75f;
        wfc.stiffness = 15;

        if (Input.GetKey(KeyCode.Space))
        {
            wfc.extremumSlip = 40f;
        }
        else
        {
            wfc.extremumSlip = 0.4f;
        }

        RL.sidewaysFriction = wfc;
        RR.sidewaysFriction = wfc;
    }

    private void AutoGears()
    {
        if (verticalInput > 0)
        {
            if (gear > 0 && gear < maxGear)
            {
                if (speed > topFWDSpeeds[gear])
                {
                    gear++;
                }
            }
            else
            {
                if (speed <= 0.1f && verticalInput > 0 && gear == 0)
                {
                    gear++;
                }
            }
        }
        else
        {
            if (gear > 1 && gear <= maxGear)
            {
                if (speed < topFWDSpeeds[gear - 1])
                {
                    gear--;
                }
            }
            else
            {
                if (speed <= 0.1f && verticalInput < 0 && gear == 1)
                {
                    gear--;
                }
            }
        }
    }

    private void ManualGears()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (gear < maxGear)
            {
                gear++;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (gear > 1)
            {
                gear--;
            }
            else if (gear == 1 && speed < 1f)
            {
                gear--;
            }
        }
    }

    private void AccelerateAuto()
    {
        if (verticalInput > 0)
        {
            if (gear != 0)
            {
                if (speed < topFWDSpeeds[gear])
                {
                    RR.motorTorque = im.vertical * motorForce;
                    RL.motorTorque = im.vertical * motorForce;
                    RR.brakeTorque = 0;
                    RL.brakeTorque = 0;
                    FR.brakeTorque = 0;
                    FL.brakeTorque = 0;
                }
                else
                {
                    RR.motorTorque = im.vertical * motorForce * limiter;
                    RL.motorTorque = im.vertical * motorForce * limiter;
                    RR.brakeTorque = 0;
                    RL.brakeTorque = 0;
                    FR.brakeTorque = 0;
                    FL.brakeTorque = 0;
                }
            }
            else
            {
                RR.brakeTorque = im.vertical * brakeForce;
                RL.brakeTorque = im.vertical * brakeForce;
                FR.brakeTorque = im.vertical * brakeForce;
                FL.brakeTorque = im.vertical * brakeForce;
            }
        }
        else if (verticalInput < 0)
        {
            if (gear != 0)
            {
                RR.brakeTorque = im.vertical * -brakeForce;
                RL.brakeTorque = im.vertical * -brakeForce;
                FR.brakeTorque = im.vertical * -brakeForce;
                FL.brakeTorque = im.vertical * -brakeForce;
            }
            else
            {
                if (speed < topREVSpeed)
                {
                    RR.motorTorque = im.vertical * revForce;
                    RL.motorTorque = im.vertical * revForce;
                    RR.brakeTorque = 0;
                    RL.brakeTorque = 0;
                    FR.brakeTorque = 0;
                    FL.brakeTorque = 0;
                }
                else
                {
                    RR.motorTorque = im.vertical * motorForce * limiter;
                    RL.motorTorque = im.vertical * motorForce * limiter;
                    RR.brakeTorque = 0;
                    RL.brakeTorque = 0;
                    FR.brakeTorque = 0;
                    FL.brakeTorque = 0;
                }
            }
        }
        else
        {
            RR.motorTorque = 0;
            RL.motorTorque = 0;
            RR.brakeTorque = 50;
            RL.brakeTorque = 50;
            FR.brakeTorque = 50;
            FL.brakeTorque = 50;
        }
    }

    private void AccelerateManual()
    {
        if (verticalInput > 0)
        {
            if (gear != 0)
            {
                if (speed < topFWDSpeeds[gear])
                {
                    RR.motorTorque = im.vertical * motorForce;
                    RL.motorTorque = im.vertical * motorForce;
                    RR.brakeTorque = 0;
                    RL.brakeTorque = 0;
                    FR.brakeTorque = 0;
                    FL.brakeTorque = 0;
                }
                else
                {
                    RR.motorTorque = im.vertical * motorForce * limiter;
                    RL.motorTorque = im.vertical * motorForce * limiter;
                    RR.brakeTorque = 0;
                    RL.brakeTorque = 0;
                    FR.brakeTorque = 0;
                    FL.brakeTorque = 0;
                }
            }
            else
            {
                if (speed < topREVSpeed)
                {
                    RR.motorTorque = im.vertical * -revForce;
                    RL.motorTorque = im.vertical * -revForce;
                    RR.brakeTorque = 0;
                    RL.brakeTorque = 0;
                    FR.brakeTorque = 0;
                    FL.brakeTorque = 0;
                }
                else
                {
                    RR.motorTorque = 0;
                    RL.motorTorque = 0;
                    RR.brakeTorque = 0;
                    RL.brakeTorque = 0;
                    FR.brakeTorque = 0;
                    FL.brakeTorque = 0;
                }
            }
        }
        else if (verticalInput < 0)
        {
            RR.brakeTorque = im.vertical * -brakeForce;
            RL.brakeTorque = im.vertical * -brakeForce;
            FR.brakeTorque = im.vertical * -brakeForce;
            FL.brakeTorque = im.vertical * -brakeForce;
        }
        else
        {
            RR.motorTorque = 0;
            RL.motorTorque = 0;
            RR.brakeTorque = 50;
            RL.brakeTorque = 50;
            FR.brakeTorque = 50;
            FL.brakeTorque = 50;
        }
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        if (maxSteerAngle > steerCurve.Evaluate(speed))
        {
            maxSteerAngle--;
        }
        if (maxSteerAngle < steerCurve.Evaluate(speed))
        {
            maxSteerAngle++;
        }

        steeringAngle = maxSteerAngle * im.horizontal;
        FR.steerAngle = steeringAngle;
        FL.steerAngle = steeringAngle;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(FL, FL_transform);
        UpdateWheelPose(FR, FR_transform);
        UpdateWheelPose(RL, RL_transform);
        UpdateWheelPose(RR, RR_transform);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _quat = _quat * Quaternion.Euler(new Vector3(0, 0, 90));

        _transform.position = _pos;
        _transform.rotation = _quat;

        if (fl_aero != null)
        {
            fl_aero.transform.localRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + steeringAngle, transform.rotation.z);
            fr_aero.transform.localRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + steeringAngle, transform.rotation.z);
        }
    }
}

