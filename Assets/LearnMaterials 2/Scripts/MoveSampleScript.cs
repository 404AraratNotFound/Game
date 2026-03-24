using System.Collections;
using UnityEngine;

/// <summary>
/// Плавно перемещает объект в указанную точку с заданной скоростью.
/// Запуск производится через метод Use().
/// </summary>
public class MoveSampleScript : SampleScript
{
    [Header("Настройки перемещения")]
    [SerializeField, Min(0f), Tooltip("Скорость перемещения в единицах в секунду")]
    private float moveSpeed = 1f;

    [SerializeField, Tooltip("Точка, в которую должен переместиться объект")]
    private Vector3 targetPosition = new Vector3(3f, 0f, 0f);

    private Coroutine moveCoroutine;

    public override void Use()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }

    private IEnumerator MoveToTargetCoroutine()
    {
        while ((transform.position - targetPosition).sqrMagnitude > 0.0001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
        moveCoroutine = null;
    }
}
