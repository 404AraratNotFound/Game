using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Запускает метод Use() у всех добавленных SampleScript.
/// </summary>
public class SampleScriptRunner : MonoBehaviour
{
    [Header("Скрипты для запуска")]
    [Tooltip("Список модулей-наследников SampleScript")]
    [SerializeField] private List<SampleScript> sampleScripts = new List<SampleScript>();

    [ContextMenu("Use All")]
    public void UseAll()
    {
        for (int i = 0; i < sampleScripts.Count; i++)
        {
            if (sampleScripts[i] != null)
            {
                sampleScripts[i].Use();
            }
            else
            {
                Debug.LogWarning($"В списке sampleScripts элемент с индексом {i} не назначен.", this);
            }
        }
    }
}
