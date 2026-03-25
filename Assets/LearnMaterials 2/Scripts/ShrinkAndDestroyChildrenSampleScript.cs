using System.Collections;
using UnityEngine;

/// <summary>
/// Плавно сжимает и удаляет все дочерние объекты у указанного target.
/// Запуск производится через метод Use().
/// </summary>
public class ShrinkAndDestroyChildrenSampleScript : SampleScript
{
    [Header("Настройки удаления дочерних объектов")]
    [SerializeField, Tooltip("Родительский объект, чьи дочерние объекты будут сжаты и удалены")]
    private Transform target;

    [SerializeField, Min(0f), Tooltip("Длительность сжатия перед удалением (в секундах)")]
    private float shrinkDuration = 1f;

    private Coroutine processCoroutine;

    public override void Use()
    {
        if (processCoroutine != null)
        {
            StopCoroutine(processCoroutine);
        }

        processCoroutine = StartCoroutine(ShrinkAndDestroyCoroutine());
    }

    private IEnumerator ShrinkAndDestroyCoroutine()
    {
        if (target == null)
        {
            Debug.LogWarning("Target не назначен в ShrinkAndDestroyChildrenSampleScript.", this);
            processCoroutine = null;
            yield break;
        }

        int childCount = target.childCount;
        if (childCount == 0)
        {
            processCoroutine = null;
            yield break;
        }

        Transform[] children = new Transform[childCount];
        Vector3[] startScales = new Vector3[childCount];

        for (int i = 0; i < childCount; i++)
        {
            children[i] = target.GetChild(i);
            startScales[i] = children[i].localScale;
        }

        if (shrinkDuration <= 0f)
        {
            for (int i = 0; i < children.Length; i++)
            {
                if (children[i] != null)
                {
                    Destroy(children[i].gameObject);
                }
            }

            processCoroutine = null;
            yield break;
        }

        float elapsed = 0f;
        while (elapsed < shrinkDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / shrinkDuration);

            for (int i = 0; i < children.Length; i++)
            {
                if (children[i] != null)
                {
                    children[i].localScale = Vector3.Lerp(startScales[i], Vector3.zero, t);
                }
            }

            yield return null;
        }

        for (int i = 0; i < children.Length; i++)
        {
            if (children[i] != null)
            {
                Destroy(children[i].gameObject);
            }
        }

        processCoroutine = null;
    }
}
