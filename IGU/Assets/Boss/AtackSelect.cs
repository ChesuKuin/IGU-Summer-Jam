using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackSelect : MonoBehaviour
{
    public bool bool1; // Первая булева переменная
    public bool bool2; // Вторая булева переменная

    private float timer;
    public float interval = 10f; // Интервал в секундах

    public Animator anim;

    void Start()
    {
        timer = interval; // Устанавливаем таймер на начальное значение
        SetRandomBool(); // Устанавливаем начальное состояние
    }

    void Update()
    {
        // Обновляем таймер
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            // Выбираем случайную булеву переменную
            SetRandomBool();
            anim.SetBool("one", bool1);
            anim.SetBool("two", bool2);
            // Сбрасываем таймер
            timer = interval;
        }
    }

    void SetRandomBool()
    {
        // Генерируем случайное число 0 или 1
        bool1 = Random.Range(0, 2) == 0;
        bool2 = !bool1; // Устанавливаем противоположное значение для другой переменной
    }
}