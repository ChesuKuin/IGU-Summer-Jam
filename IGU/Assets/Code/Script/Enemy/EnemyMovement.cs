using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public Collider2D GroundCheck;
    public Collider2D PlayerDetect;
    public float speed = 0.5f;
    public int direction = -1;
    public bool attack = false;
    private bool grounded;
    private bool isFacingRight = false;
    public GameObject Exit;

    public int MaxHealth = 2;
    public int CurrentHealth;

    public AudioSource DeathMonster;

    public GameObject Monster;

    public bool CanBeAttacked = true;

    void Start()
    {
        Exit.SetActive(true);
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("attack", attack);
        if (!attack)
        {
            Flip();
            transform.position += Vector3.right * direction * speed * Time.deltaTime;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider == GroundCheck && collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }

        if (collision.otherCollider == PlayerDetect && collision.gameObject.CompareTag("Player"))
        {
            attack = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.otherCollider == GroundCheck && collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }

        if (collision.otherCollider == PlayerDetect && collision.gameObject.CompareTag("Player"))
        {
            attack = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Damage"))
        {
            if (CanBeAttacked)
            {
                anim.SetBool("Hit", true);

                TakeDamage(1);
                StartCoroutine(WaitUntilDamage());
            }
            
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }
        if (CurrentHealth <= 0)
        {
            Destroy(Monster);
            Exit.SetActive(false);
        }

    }
    private void OnDrawGizmos()
    {
        // Visualize GroundCheck and PlayerDetect colliders in the editor
        if (GroundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(GroundCheck.bounds.center, GroundCheck.bounds.size);
        }

        if (PlayerDetect != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(PlayerDetect.bounds.center, PlayerDetect.bounds.size);
        }
    }
    private void Flip()
    {
        if (!grounded)
        {
            direction *= -1;
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

    }
    private IEnumerator WaitUntilDamage()
    {
        CanBeAttacked = false;
        yield return new WaitForSeconds(0.2f);

        anim.SetBool("Hit", false);
        yield return new WaitForSeconds(1f);
        CanBeAttacked = true;
    }
}
