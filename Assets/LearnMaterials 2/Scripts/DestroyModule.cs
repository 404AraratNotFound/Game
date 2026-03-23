using System.Collections;
using UnityEngine;

[HelpURL("https://docs.google.com/document/d/1RMamVxE-yUpSfsPD_dEa4-Ak1qu6NTo83qY1O4XLxUY/edit?usp=sharing")]
public class DestroyModule : MonoBehaviour
{
    [Header("Настройки удаления")]
    [SerializeField, Min(0f), Tooltip("Задержка между удалением дочерних объектов (в секундах)")]
    private float destroyDelay = 0.2f;

    [SerializeField, Min(0), Tooltip("Минимальное количество дочерних объектов, которое должно остаться")]
    private int minimalDestroyingObjectsCount;

    private Transform myTransform;
    private bool isActivated;

    private void Awake()
    {
        myTransform = transform;
    }

    public void ActivateModule()
    {
        if (isActivated)
        {
            return;
        }

        isActivated = true;
        StartCoroutine(DestroyRandomChildObjectCoroutine());
    }

    private IEnumerator DestroyRandomChildObjectCoroutine()
    {
        while (myTransform.childCount > minimalDestroyingObjectsCount)
        {
            int index = Random.Range(0, myTransform.childCount);
            Destroy(myTransform.GetChild(index).gameObject);
            yield return new WaitForSeconds(destroyDelay);
        }

        Destroy(gameObject);
    }
}
