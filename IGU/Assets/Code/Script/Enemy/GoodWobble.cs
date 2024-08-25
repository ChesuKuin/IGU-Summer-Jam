using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodWobble : MonoBehaviour
{
    public Collider2D GroundCheck;
    public float speed = 0.5f;
    public int direction = -1;
    private bool grounded;
    private bool isFacingRight = false;

    void Update()
    {
        Flip();
        transform.position += Vector3.right * direction * speed * Time.deltaTime;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider == GroundCheck && collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.otherCollider == GroundCheck && collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
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
