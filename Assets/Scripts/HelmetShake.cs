using System;
using UnityEngine;
using DG.Tweening;

public class HelmetShake : MonoBehaviour
{
    public Transform cam;
    public Vector3 posStrength;
    public Vector3 rotStrength;

    private static event Action Shake;

    public CarControl cc;

    public void Update()
    {
        Invoke();
    }

    public static void Invoke()
    {
        Shake?.Invoke();
    }

    private void OnEnable() => Shake += CameraShake;
    private void OnDisable() => Shake -= CameraShake;

    private void CameraShake()
    {
        cam.DOComplete();
        cam.DOShakePosition(0.2f, posStrength * cc.speed);
        cam.DOShakeRotation(0.2f, rotStrength * cc.speed);
    }
}
