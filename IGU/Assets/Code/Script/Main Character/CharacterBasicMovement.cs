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

    [SerializeField] private Rigidbody2D rb;
    public bool grounded; //is on ground
    public bool running = false; // not running
    public AudioSource JumpS;
    public AudioSource Walk;

    private void Update()
    {

        //Walking
        horizontal = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(horizontal));
        if (horizontal == Input.GetAxisRaw("Horizontal"))
        {
            Walk.Play();
        }

        //Jumping
        Jump();

        //Flipping the character
        Flip();

    }

    //walking
    private void FixedUpdate()
    {
        transform.position += new Vector3(horizontal * speed * Time.fixedDeltaTime, 0, 0);
    }

    //Checking if the character is grounded
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    //Checking if the character is grounded
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }




    //Jumping
    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                JumpS.Play();
            }
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
}