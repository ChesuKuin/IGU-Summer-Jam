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

    void Start()
    {
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
}
