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

    private Rigidbody2D rb;

    // Knockback force and duration
    public float knockbackDistance = 2f;
    public float knockbackDuration = 0.2f;

    // To track knockback status
    private bool isKnockedBack = false;

    void Start()
    {
        CurrentHealth = MaxHealth;
        UpdateHearts();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        anim.SetBool("Hurt", Hurt);
    }

    void UpdateHearts()
    {
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
        if (collision.gameObject.CompareTag("Enemy") && !isKnockedBack)
        {
            Debug.Log("Collision with Enemy detected");
            Hurt = true;
            TakeDamage(1);

            // Calculate knockback direction
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;

            // Apply knockback
            StartCoroutine(ApplyKnockback(knockbackDirection));
        }
    }

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

    private IEnumerator ApplyKnockback(Vector2 direction)
    {
        isKnockedBack = true;

        // Calculate the knockback end position
        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + direction * knockbackDistance;

        float elapsed = 0f;

        while (elapsed < knockbackDuration)
        {
            transform.position = Vector2.Lerp(startPosition, endPosition, elapsed / knockbackDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition; // Ensure final position is set
        isKnockedBack = false;
    }
}