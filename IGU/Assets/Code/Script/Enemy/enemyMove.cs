using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public float speed = 0.2f;
    public int direction = -1;
    public float rayLength;
    private SpriteRenderer spriteRenderer;
    public LayerMask ignoreLayer;
    public bool attack = false;
    RaycastHit2D hitGround;
    RaycastHit2D hitWall;
    RaycastHit2D hitPlayer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = direction == 1;
    }

    private void FixedUpdate()
    {
        Vector2 groundRayOrigin = (Vector2)transform.position + Vector2.down * 1f + Vector2.right * direction * 1f;
        Vector2 wallRayOrigin = (Vector2)transform.position + Vector2.right * direction * 0.75f;
        Vector2 hitPlayerOrigin = (Vector2)transform.position + Vector2.right * direction * 0.1f;
        hitGround = Physics2D.Raycast(groundRayOrigin, Vector2.down, rayLength, ~ignoreLayer);
        hitWall = Physics2D.Raycast(wallRayOrigin, Vector2.right * direction, 0.1f, ~ignoreLayer);
        hitPlayer = Physics2D.Raycast(wallRayOrigin, Vector2.right * direction, 4f);

        if (hitPlayer.collider != null && hitPlayer.collider.tag == "Player")
        {
            attack = true;
        }
        else { attack = false; }
        anim.SetBool("attack", attack);

        if (!attack)
        {


            if (hitGround.collider == null || hitWall.collider != null)
            {
                direction *= -1;
                spriteRenderer.flipX = direction == 1;
            }
            transform.position += Vector3.right * direction * speed * Time.deltaTime;
        }
        

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector2 groundRayOrigin = (Vector2)transform.position + Vector2.down * 1f + Vector2.right * direction * 1f;
        Gizmos.DrawLine(groundRayOrigin, groundRayOrigin + Vector2.down * rayLength);

        Vector2 wallRayOrigin = (Vector2)transform.position + Vector2.right * direction * 0.75f;
        Gizmos.DrawLine(wallRayOrigin, wallRayOrigin + Vector2.right * direction * 0.1f);

        Vector2 hitPlayerOrigin = (Vector2)transform.position + Vector2.right * direction * 0.4f;
        Gizmos.DrawLine(hitPlayerOrigin, hitPlayerOrigin + Vector2.right * direction * 0.1f);
    } 
}
