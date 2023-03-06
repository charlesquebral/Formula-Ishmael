using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDControl : MonoBehaviour
{
    [Header("Gameplay")]
    public GameObject gameFrame;
    public TimeControl tc;
    public DashControl dc;
    public DeltaTimeControl dtc;

    public Color originalCol, bestCol, invalidCol;
    public Image currFrame, bestFrame;
    public TextMeshProUGUI lap, best, time, delta;

    [Header("PostGameplay")]
    public GameObject postGameFrame;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tc.valid)
        {
            gameFrame.SetActive(true);
            postGameFrame.SetActive(false);

            DisplayGameFrame();
        }
        else
        {
            gameFrame.SetActive(false);
            postGameFrame.SetActive(true);

            DisplayPostGameFrame();
        }
    }

    void DisplayGameFrame()
    {
        lap.text = "LAP " + tc.lapsUsed.ToString() + " / " + tc.lapsAvailable.ToString();
        best.text = "BEST: " + dc.bestTimeText.text;
        time.text = "CURRENT: " + dc.timeText.text;
        delta.text = dtc.deltaText;

        if (dc.enable)
        {
            if (dc.GetComponent<TLimitControl>().goodTime)
            {
                currFrame.color = originalCol;
            }
            else
            {
                currFrame.color = invalidCol;
            }
        }
        else
        {
            if (dc.lastLapGood)
            {
                currFrame.color = originalCol;
            }
            else
            {
                currFrame.color = invalidCol;
            }
        }
    }

    void DisplayPostGameFrame()
    {

    }

    public IEnumerator ChangeToPurple()
    {
        bestFrame.color = bestCol;
        yield return new WaitForSeconds(5);
        bestFrame.color = originalCol;
    }
}
