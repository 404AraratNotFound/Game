using UnityEngine;

/// <summary>
/// Модуль создания нескольких копий префаба по прямой линии
/// </summary>
public class CopyModule : SampleScript
{
    [Header("Copy Module Settings")]
    [Tooltip("Префаб, который нужно копировать")]
    [SerializeField] private GameObject prefabToCopy;

    [Tooltip("Количество создаваемых копий")]
    [SerializeField][Min(1)] private int count = 5;

    [Tooltip("Расстояние между копиями")]
    [SerializeField] private float spacing = 2f;

    [Tooltip("Направление, в котором будут создаваться копии")]
    [SerializeField] private Vector3 direction = Vector3.right;

    public override void Use()
    {
        if (prefabToCopy == null)
        {
            Debug.LogError("CopyModule: PrefabToCopy не назначен!", this);
            return;
        }

        Vector3 startPos = transform.position;

        for (int i = 1; i <= count; i++)
        {
            Vector3 spawnPosition = startPos + (direction.normalized * spacing * i);
            Instantiate(prefabToCopy, spawnPosition, Quaternion.identity);
        }

        Debug.Log($"CopyModule: Создано {count} копий префаба '{prefabToCopy.name}'");
    }
}