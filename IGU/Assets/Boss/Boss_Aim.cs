using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Aim : MonoBehaviour
{   
    public Transform player; // Ссылка на игрока
    public float range = 10f;
    public LayerMask targetLayer;
    public Transform firePoint; // Точка, из которой будут запускаться снаряды
    public GameObject projectilePrefab; // Префаб снаряда
    public float raycastDelay = 2f; // Задержка перед запуском снарядов
    public float projectileSpeed = 10f;
    
    private RaycastHit hit;

    void OnEnable()
    {
        StartCoroutine(ShootProjectileFan());
    }
    
    void Update()
    {   
        Vector2 direction = (player.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range, targetLayer);
        if (hit.collider != null)
        {
            // Рейкаст попал в цель
            Debug.DrawRay(transform.position, direction * range, Color.green);
        }
    }

    private IEnumerator ShootProjectileFan()
    {
        // Ожидаем задержку перед запуском снарядов
        yield return new WaitForSeconds(raycastDelay);

        // Параметры веера снарядов
        int numProjectiles = 5; // Количество снарядов
        float spreadAngle = 20f; // Угол рассеивания снарядов

        // Вычисляем угол направления рейкаста
        Vector2 raycastDirection = (hit.collider != null ? (Vector2)hit.point - (Vector2)transform.position : (Vector2)player.position - (Vector2)transform.position).normalized;
        float baseAngle = Mathf.Atan2(raycastDirection.y, raycastDirection.x) * Mathf.Rad2Deg;

        for (int i = 0; i < numProjectiles; i++)
        {
            // Вычисляем угол рассеивания
            float angle = (i - (numProjectiles - 1) / 2f) * spreadAngle;
            float projectileAngle = baseAngle + angle;

            // Создаем снаряд и настраиваем его начальное положение и ориентацию
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.Euler(0, 0, projectileAngle));
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Направляем снаряд в сторону рейкаста
                Vector2 firePoint2D = (Vector2)firePoint.position;
                Vector2 targetDirection = ((hit.collider != null ? (Vector2)hit.point : (Vector2)player.position) - firePoint2D).normalized;
                
                // Применяем угловое направление для снаряда
                Vector2 projectileDirection = new Vector2(Mathf.Cos(Mathf.Deg2Rad * projectileAngle), Mathf.Sin(Mathf.Deg2Rad * projectileAngle)).normalized;
                rb.velocity = projectileDirection * projectileSpeed;
            }

            // Ожидаем немного, чтобы избежать мгновенного запуска всех снарядов
            yield return null;
        }
    }
}