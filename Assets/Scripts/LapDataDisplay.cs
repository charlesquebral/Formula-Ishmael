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

    [Header("Etc")]
    public TimeControl tc;
    public HUDControl hc;
    public int childIndex;

    // Start is called before the first frame update
    void Start()
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
        }
    }
}
