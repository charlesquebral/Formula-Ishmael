using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Ghost : ScriptableObject
{
    public bool isRecord;
    public bool isReplay;
    public float recordFrequency;

    public List<float> timeStamp;
    public List<Vector3> position;
    public List<Vector3> rotation;

    public List<float> timeStampCurr;
    public List<Vector3> positionCurr;
    public List<Vector3> rotationCurr;

    public void Awake()
    {

    }

    public void ResetData()
    {
        timeStampCurr.Clear();
        positionCurr.Clear();
        rotationCurr.Clear();
    }

    public void ResetBestData()
    {
        timeStamp.Clear();
        position.Clear();
        rotation.Clear();
    }

    public void AssignNewData()
    {
        timeStamp = new List<float>(timeStampCurr);
        position = new List<Vector3>(positionCurr);
        rotation = new List<Vector3>(rotationCurr);
    }
}
