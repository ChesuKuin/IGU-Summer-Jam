using System.Collections;
using UnityEngine;

public class PowMirror : MonoBehaviour
{
    public GameObject Pow;
    [SerializeField] private Animator anim;
    public GameObject MageMirror;

    void Start()
    {
        Pow.SetActive(false);
    }

    void Update()
    {
        Changling();
    }

    private void Changling()
    {
        if (MageMirror.activeSelf == true && Input.GetKey(KeyCode.T))
        {
            anim.SetInteger("Pow", 1);

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("pow"))
            {
                StartCoroutine(ExecutePowSequence());
            }
        }
    }

    private IEnumerator ExecutePowSequence()
    {
        anim.SetInteger("Pow", 2);
        // Transition to the second part of the "Pow" animation
        yield return new WaitForSeconds(0.4f);
        Pow.SetActive(true);


        // Wait for the second animation to finish before proceeding
        yield return new WaitForSeconds(0.4f);

        // Transition to the third part of the "Pow" animation and activate "Pow"
        anim.SetInteger("Pow", 3);
        Pow.SetActive(false);


        yield return new WaitForSeconds(0.4f);

        
        anim.SetInteger("Pow", 0);
    }
}

