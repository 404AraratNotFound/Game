using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSampleScript : SampleScript
{
    [Header("Rotation Settings")]

    [SerializeField]
    [Tooltip("—корость вращени€ (градусов в секунду)")]
    [Min(0.01f)]
    private float rotationSpeed = 10f;

    [SerializeField]
    [Tooltip("”гол поворота по ос€м (в градусах)")]
    private Vector3 rotationAngles = new Vector3(0, 90, 0);

    private bool isRotating = false;

    public override void Use()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateCoroutine());
        }
    }

    private IEnumerator RotateCoroutine()
    {
        isRotating = true;

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(rotationAngles);

        float angle = rotationAngles.magnitude;
        float duration = angle / rotationSpeed;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            yield return null;
        }

        transform.rotation = targetRotation;
        isRotating = false;
    }
}
