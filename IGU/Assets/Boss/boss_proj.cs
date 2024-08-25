using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_proj : MonoBehaviour
{
    // Start is called before the first frame update

    public float lifetime = 5f; // Время жизни снаряда

    void Start()
    {
        // Запускаем таймер на удаление снаряда
        Destroy(gameObject, lifetime);
    }
}
