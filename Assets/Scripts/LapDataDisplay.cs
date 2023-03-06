using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LapDataDisplay : MonoBehaviour
{
    [Header("Text")]
    public TextMeshProUGUI lap;
    public TextMeshProUGUI time;
    public TextMeshProUGUI valid;
    public TextMeshProUGUI avgSpeed;
    public TextMeshProUGUI dist;
    public TextMeshProUGUI timeT;
    public TextMeshProUGUI timeB;
    public TextMeshProUGUI timeC;

    [Header("Etc")]
    public TimeControl tc;
    public HUDControl hc;
    public DataGather dg;
    public int childIndex;

    // Start is called before the first frame update
    void Awake()
    {
        childIndex = transform.GetSiblingIndex();
    }

    // Update is called once per frame
    void Update()
    {
        if (tc.lapNum.Count > childIndex)
        {
            lap.text = tc.lapNum[childIndex];
            time.text = tc.times[childIndex];
            valid.text = tc.validated[childIndex];
            avgSpeed.text = dg.avgSpeed[childIndex];
            dist.text = dg.distTraveled[childIndex];
            timeT.text = dg.timeThrot[childIndex];
            timeB.text = dg.timeBrake[childIndex];
            timeC.text = dg.timeCoast[childIndex];
        }
    }
}
