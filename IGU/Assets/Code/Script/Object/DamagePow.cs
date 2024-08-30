using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePow : MonoBehaviour
{
    public GameObject DamagePoww;
    [SerializeField] private Animator anim;
    public GameObject MageMirror;
    public bool CanAttack = true;

    void Start()
    {
        DamagePoww.SetActive(false);
    }

    void Update()
    {
        if (CanAttack)
        {
            Damage();
        }
    }

    private void Damage()
    {
        if (MageMirror.activeSelf == true && Input.GetKey(KeyCode.L))
        {
            anim.SetInteger("Damage", 2);
            StartCoroutine(ExecutePowDamage());
        }
    }

    private IEnumerator ExecutePowDamage()
    {
        CanAttack = false;
        DamagePoww.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        DamagePoww.SetActive(false);

        yield return new WaitForSeconds(0.4f);

        anim.SetInteger("Damage", 0);

        yield return new WaitForSeconds(1f);

        CanAttack = true;
    }

}
