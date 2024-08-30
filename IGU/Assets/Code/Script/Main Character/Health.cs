using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
    public CharacterBasicMovement CharacterBasicMovement;
    private Rigidbody2D rb;

    // Knockback force and duration
    public float knockbackForce;
    public float knockbackDuration;
    public float knockbackCounter;

    // To track knockback status
    private bool isKnockedBack = false;

    public bool Death = false;
    public GameObject DeathScreen;
    public bool Dead = false;
    public GameObject Jump;

    public GameObject Tip1;

    public AudioSource HurtS;
    public AudioSource DeadS;
    public AudioSource TpS;

    public string SceneName;

    public bool Hitframe = false;

    public bool Died = false;

    void Start()
    {
        CurrentHealth = MaxHealth;
        UpdateHearts();
        rb = GetComponent<Rigidbody2D>();
        CharacterBasicMovement = GetComponent<CharacterBasicMovement>();
        DeathScreen.SetActive(false);
        Tip1.SetActive(false);
    }

    void Update()
    {
        anim.SetBool("Hurt", Hurt);
        if (Death && Input.GetKey(KeyCode.J))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
            Time.timeScale = 1;
        }
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
            if (!Hitframe)
            {
                Debug.Log("Collision with Enemy detected");
                Hurt = true;
                TakeDamage(1);
                Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;

                StartCoroutine(ApplyKnockback(knockbackDirection));

                HurtS.Play();
            }
        }
        if (collision.gameObject.CompareTag("Ow"))
        {
            TakeDamage(4);
            Tip1.SetActive(true);
        }
        if (collision.gameObject.CompareTag("Beam"))
        {
            if (!Hitframe)
            {
                Debug.Log("Collision with Enemy detected");
                Hurt = true;
                TakeDamage(1);
                Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;

                StartCoroutine(ApplyKnockback(knockbackDirection));

                HurtS.Play();
            }
        }
        if (collision.gameObject.CompareTag("Insta"))
        {
            if (!Hitframe)
            {
                Debug.Log("Collision with Enemy detected");
                TakeDamage(3);
                Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;

                StartCoroutine(ApplyKnockback(knockbackDirection));

                HurtS.Play();
            }
        }
        if (collision.gameObject.CompareTag("Tp"))
        {
            TpS.Play();
            SceneManager.LoadScene(SceneName);
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

            DeadS.Play();
            Death = true;
            anim.SetBool("Death", true);
            DeathScreen.SetActive(true);
            anim.SetBool("Death", false);
            Time.timeScale = 0;
            Died = true;

        }

    }

    private IEnumerator ApplyKnockback(Vector2 direction)
    {
        CharacterBasicMovement.enabled = false;
        isKnockedBack = true;
        knockbackCounter = knockbackDuration;

        while (knockbackCounter >= 0)
        {
            rb.velocity = direction.normalized * knockbackForce;
            knockbackCounter -= Time.deltaTime;
            yield return null; 
        }

        rb.velocity = Vector2.zero;
        CharacterBasicMovement.enabled = true;
        isKnockedBack = false;
    }
    private IEnumerator HitFrameWait()
    {
        Hitframe = true;
        yield return new WaitForSeconds(1f);
        Hitframe = false;

    }
}