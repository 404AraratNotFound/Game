using System.Collections;
using UnityEngine;

/// <summary>
/// Модуль, который плавно изменяет масштаб объекта.
/// </summary>
[HelpURL("https://docs.google.com/document/d/1rdTEVSrCcYOjqTJcFCHj46RvnbdJhmQUb3gHMDhVftI/edit?usp=sharing")]
public class ScalerModule : MonoBehaviour
{
    [Header("Настройки масштабирования")]
    [Tooltip("Целевой масштаб, к которому будет изменяться объект.")]
    [SerializeField] private Vector3 targetScale = new Vector3(2, 2, 2);

    [Tooltip("Скорость изменения масштаба.")]
    [SerializeField] private float changeSpeed = 1f;

    private Vector3 defaultScale;
    private Transform myTransform;
    private bool toDefault;

    private void Start()
    {
        myTransform = transform;
        defaultScale = myTransform.localScale;
        toDefault = false;
    }

    /// <summary>
    /// Запускает процесс масштабирования объекта.
    /// </summary>
    public void ActivateModule()
    {
        Vector3 target = toDefault ? defaultScale : targetScale;
        StopAllCoroutines();
        StartCoroutine(ScaleCoroutine(target));
        toDefault = !toDefault;
    }

    /// <summary>
    /// Возвращает объект к исходному масштабу.
    /// </summary>
    public void ReturnToDefaultState()
    {
        toDefault = true;
        ActivateModule();
    }

    private IEnumerator ScaleCoroutine(Vector3 target)
    {
        Vector3 start = myTransform.localScale;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * changeSpeed;
            myTransform.localScale = Vector3.Lerp(start, target, t);
            yield return null;
        }

        myTransform.localScale = target;
    }

    /// <summary>
    /// Проверяет корректность введённых значений в инспекторе.
    /// </summary>
    private void OnValidate()
    {
        if (changeSpeed < 0)
            changeSpeed = 0;

        if (targetScale.x < 0) targetScale.x = 0;
        if (targetScale.y < 0) targetScale.y = 0;
        if (targetScale.z < 0) targetScale.z = 0;
    }
}
