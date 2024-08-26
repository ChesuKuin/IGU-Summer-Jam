using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private bool CanAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        if (CanAttack)
        {
            StartCoroutine(BeamAttack());
        }
    }

    private IEnumerator BeamAttack()
    {
        CanAttack = false; // Ensure this coroutine doesn't start multiple times

        anim.SetInteger("Attack", 1);
        yield return new WaitForSeconds(1f);
        anim.SetInteger("Attack", 2);
        yield return new WaitForSeconds(2f);
        anim.SetInteger("Attack", 3);
        yield return new WaitForSeconds(0.1f);
        anim.SetInteger("Attack", 0);

        // If you want to reset for another attack, you can either call Wait again or directly reset CanAttack here.
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        CanAttack = true;
    }
}