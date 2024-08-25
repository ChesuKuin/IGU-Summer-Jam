using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBasicMovement : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private float horizontal; //horizontal movement
    private float speed = 4f;
    private float jumpingPower = 12f;
    private bool isFacingRight = true;
    public LayerMask ignoreLayer;
    public Vector3 boxSize;
    public float maxDistance;
    [SerializeField] private Rigidbody2D rb;
     //is on ground
    public bool running = false; // not running


    private void Update()
    {

        //Walking
        horizontal = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(horizontal));

        //Jumping
        

        //Flipping the character
        Jump();
        Flip();

    }

    //walking
    private void FixedUpdate()
    {
        transform.position += new Vector3(horizontal * speed * Time.fixedDeltaTime, 0, 0);
        
    }

    //Checking if the character is grounded




    //Jumping
    private void Jump()
    {
         if (Input.GetButtonDown("Jump") && Groundcheck())
        {
        
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
    }

    //Flipping the character
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

    }
     private bool Groundcheck()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0f, Vector2.down, maxDistance, ~ignoreLayer)) 
        {   
            return true;
        }
        else { return false;}
    }
    private void OnDrawGizmos()
    {
    Gizmos.color = Color.red;
    // Используйте transform.position вместо TransformPosition
    Gizmos.DrawCube(transform.position - (Vector3)transform.up * maxDistance, boxSize);
    }
}