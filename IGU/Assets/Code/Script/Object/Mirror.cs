using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{

    [SerializeField] private Animator anim;
    public GameObject MageMirror;

    private bool OnWall;
    // Start is called before the first frame update

    void Start()
    {
    OnWall = true;
}

    // Update is called once per frame
    void Update()
    {
        if (MageMirror.activeSelf == true && OnWall == true)
        {
            anim.SetFloat("PickedUp", 1);
}
            
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Round"))
        {
            anim.SetFloat("PickedUp", 0);

        OnWall = false;
    }
    }
}
