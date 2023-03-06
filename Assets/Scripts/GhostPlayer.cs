using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlayer : MonoBehaviour
{
    public Ghost ghost;
    public float timerVal;
    public int index1;
    public int index2;
    // Start is called before the first frame update
    void Awake()
    {
        timerVal = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timerVal += Time.unscaledDeltaTime;

        if (ghost.isReplay)
        {
            GetIndex();
            SetTransform();
        }
    }

    void GetIndex()
    {
        for (int i = 0; i < ghost.timeStamp.Count - 1; i++)
        {
            if (ghost.timeStamp[i] == timerVal)
            {
                index1 = i;
                index2 = i;
                return;
            }
            else if (ghost.timeStamp[i] < timerVal && timerVal < ghost.timeStamp[i + 1])
            {
                index1 = 1;
                index2 = i + 1;
                return;
            }
        }

        index1 = ghost.timeStamp.Count - 1;
        index2 = ghost.timeStamp.Count - 1;
    }

    void SetTransform()
    {
        if (index1 == index2)
        {
            transform.position = ghost.position[index1];
            transform.eulerAngles = ghost.rotation[index1];
        }
        else
        {
            float interpolationFactor = (timerVal - ghost.timeStamp[index1]) / (ghost.timeStamp[index2] - ghost.timeStamp[index1]);

            transform.position = Vector3.Lerp(ghost.position[index1], ghost.position[index2], interpolationFactor);
            transform.eulerAngles = Vector3.Lerp(ghost.rotation[index1], ghost.rotation[index2], interpolationFactor);
        }
    }
}
