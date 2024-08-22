using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{

    [SerializeField] private Animator anim;
    public GameObject MageMirror;
    private bool OnWall = true;
    // Start is called before the first frame update

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (MageMirror.activeSelf == true && OnWall == true)
        {
            anim.SetFloat("PickedUp", 1);
            OnWall = false;
}
            
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Round"))
        {
            anim.SetFloat("PickedUp", 0);
        }
    }
}
