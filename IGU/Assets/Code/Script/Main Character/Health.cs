using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth = 4;
    public int CurrentHealth;
    public bool Hurt;
    [SerializeField] private Animator anim;
    public GameObject[] hearts;

    public Sprite fullHeart;
    public Sprite emptyHeart;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        CurrentHealth = MaxHealth;
        UpdateHearts();
    }

    void Update()
    {
        anim.SetBool("Hurt", Hurt);
    }

    void UpdateHearts()
    {
        // Set all hearts to emptyHeart by default
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < CurrentHealth)
            {
                hearts[i].GetComponent<SpriteRenderer>().sprite = fullHeart;
            }
            else
            {
                hearts[i].GetComponent<SpriteRenderer>().sprite = emptyHeart;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Hurt = true;
            TakeDamage(1);
        }
    }

    //Checking if the character is grounded
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Hurt = false;
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }
        UpdateHearts();
        if (CurrentHealth <= 0)
        {
            Debug.Log("Ow");
        }
    }
}

