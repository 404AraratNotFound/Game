using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Задаёт указанным объектам значение activeSelf, равное state
/// </summary>
[HelpURL("https://docs.google.com/document/d/1GP4_m0MzOF8L5t5pZxLChu3V_TFIq1czi1oJQ2X5kpU/edit?usp=sharing")]
public class GameObjectActivator : MonoBehaviour
{
    [Header("Объекты для активации/деактивации")]
    [Tooltip("Список объектов, которыми будет управлять этот модуль")]
    [SerializeField]
    private List<StateContainer> targets = new List<StateContainer>();

    [Header("Отладка")]
    [Tooltip("Включить отрисовку Gizmos в сцене (линии и сферы)")]
    [SerializeField]
    private bool debug = false;


    private void Awake()
    {
        foreach (var item in targets)
        {
            if (item != null && item.targetGO != null)
            {
                item.defaultValue = item.targetGO.activeSelf;
            }
        }
    }

    [ContextMenu("1. Activate Module")]
    public void ActivateModule()
    {
        SetStateForAll();
    }

    [ContextMenu("2. Return To Default State")]
    public void ReturnToDefaultState()
    {
        foreach (var item in targets)
        {
            if (item != null && item.targetGO != null)
            {
                item.targetState = item.defaultValue;
                item.targetGO.SetActive(item.defaultValue);
            }
        }
    }

    private void SetStateForAll()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null && targets[i].targetGO != null)
            {
                targets[i].targetGO.SetActive(targets[i].targetState);
                targets[i].targetState = !targets[i].targetState;
            }
            else
            {
                Debug.LogError($"Элемент {i} равен null. Вероятно, была утеряна ссылка. Источник: {gameObject.name}");
            }
        }
    }

    #region Материал ещё не изучен
    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawSphere(transform.position, 0.3f);

            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] != null && targets[i].targetGO != null)
                {
                    Gizmos.color = targets[i].targetState ? Color.green : Color.red;
                    Gizmos.DrawLine(transform.position, targets[i].targetGO.transform.position);
                }
                else
                {
                    Debug.LogError($"Элемент {i} равен null. Вероятно, была утеряна ссылка. Источник: {gameObject.name}");
                }
            }
        }
    }
    #endregion
}

#region Материал ещё не изучен
[System.Serializable]
public class StateContainer
{
    [Tooltip("Объект, которому нужно задать состояние (включи/выключи)")]
    public GameObject targetGO;

    [Tooltip("Целевое состояние. Если отмечено — объект будет включён")]
    public bool targetState = false;

    [HideInInspector]
    public bool defaultValue;
}
#endregion