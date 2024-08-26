using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    public float speed = 5f;
    public Vector2 targetPosition;
    private Vector2 originalPosition;
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    private bool isShooting = false;
    private bool canDealDamage = false; // Flag to determine if the ball can deal damage

    void Start()
    {
        originalPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        spriteRenderer.enabled = false; // Initially hide the ball
    }

    void Update()
    {
        // Check if the left mouse button is clicked and the ball is not already shooting
        if (Input.GetMouseButtonDown(0) && !isShooting)
        {
            isShooting = true;
            canDealDamage = true; // Enable damage when the ball is shot
            spriteRenderer.enabled = true; // Make the ball visible when shooting
            StartCoroutine(ShootAndReturn());
        }
    }

    private IEnumerator ShootAndReturn()
    {
        // Move towards the target position
        while ((Vector2)transform.position != targetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.4f); // Delay before returning

        spriteRenderer.enabled = false; // Hide the ball when it starts returning
        canDealDamage = false; // Disable damage when the ball is returning

        // Move back to the original position
        while ((Vector2)transform.position != originalPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, originalPosition, speed * Time.deltaTime);
            yield return null;
        }

        isShooting = false; // Allow shooting again after the ball returns and disappears
    }

    // Example method to check if the ball can deal damage
    public bool CanDealDamage()
    {
        return canDealDamage;
    }
}