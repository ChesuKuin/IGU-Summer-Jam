using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;  // Array of waypoints
    [SerializeField] private float moveSpeed = 2f;   // Speed at which the boss moves

    public bool CanBeAttacked = true;
    private int currentWaypointIndex = 0;            // Index of the current waypoint
    private bool notPow = true;

    public int MaxHealth = 5;
    public int CurrentHealth;

    public GameObject Done;
    public GameObject SecretI;


    public bool DoneB = false;
    public bool SecretB = false;

    [SerializeField] private Animator anim;

    void Start()
    {
        Done.SetActive(false);
        SecretI.SetActive(false);
    }
    void Update()
    {
        if (notPow)
        {
            MoveBoss();
        }
        if (DoneB && Input.GetKey(KeyCode.R))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
            Time.timeScale = 1;
        }
        if (SecretB && Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("Level 1");
            Time.timeScale = 1;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Damage"))
        {
            if (CanBeAttacked)
            {
                anim.SetBool("Hurt", true);

                TakeDamage(1);
                StartCoroutine(WaitUntilDamage());
            }

        }
        if (other.CompareTag("Pow"))
        {
            notPow = false;
            anim.SetBool("Secret", true);
                StartCoroutine(Secret());

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
            Done.SetActive(true);
            Time.timeScale = 0;
            DoneB = true;
        }

    }
    private IEnumerator WaitUntilDamage()
    {
        CanBeAttacked = false;
        yield return new WaitForSeconds(0.2f);

        anim.SetBool("Hurt", false);
        yield return new WaitForSeconds(1f);
        CanBeAttacked = true;
    }
    private IEnumerator Secret()
    {
        yield return new WaitForSeconds(1.3f);
        anim.SetBool("Secret", false);
        yield return new WaitForSeconds(0.3f);
        SecretI.SetActive(true);
        SecretB = true;
    }
    private void MoveBoss()
    {
        // If there are no waypoints, do nothing
        if (waypoints.Length == 0) return;

        // Move the boss towards the current waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);

        // Check if the boss has reached the waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            // If so, move to the next waypoint
            currentWaypointIndex++;

            // Loop back to the first waypoint if the boss reaches the last one
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
    }
}