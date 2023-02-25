using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorControl : MonoBehaviour
{
    public CarControl cc;

    public Camera leftCam, rightCam;
    public Renderer leftPlane, rightPlane;

    // Start is called before the first frame update
    void Awake()
    {
        Invoke("DoMirrors", 0.1f);
    }

    void DoMirrors()
    {
        RenderTexture leftTex = new RenderTexture(360, 180, 1);
        leftCam.targetTexture = leftTex;
        leftPlane.material.SetTexture("_MainTex", leftTex);
        leftCam.farClipPlane = 50;

        RenderTexture rightTex = new RenderTexture(360, 180, 1);
        rightCam.targetTexture = rightTex;
        rightPlane.material.SetTexture("_MainTex", rightTex);
        rightCam.farClipPlane = 50;
    }

    private void Update()
    {

    }
}
