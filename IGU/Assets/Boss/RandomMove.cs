using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public GameObject[] points; // Массив объектов назначения
    public float moveSpeed = 5f; // Скорость перемещения
    private Transform targetPoint; // Текущая целевая точка

    void Start()
    {
        if (points.Length > 0)
        {
            // Выбираем случайную начальную точку
            SetRandomTarget();
        }
    }

    void Update()
    {
        if (targetPoint != null)
        {
            // Перемещаем объект к целевой точке
            MoveTowardsTarget();

            // Проверяем, достигли ли мы цели
            if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
            {
                // Устанавливаем новую целевую точку
                SetRandomTarget();
            }
        }
    }

    private void MoveTowardsTarget()
    {
        // Перемещение объекта к целевой точке
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);
    }

    private void SetRandomTarget()
    {
        // Выбираем случайный объект из массива и устанавливаем его трансформ в качестве целевой точки
        GameObject randomPoint = points[Random.Range(0, points.Length)];
        targetPoint = randomPoint.transform;
    }
}
