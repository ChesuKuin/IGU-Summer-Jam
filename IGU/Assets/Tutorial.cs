using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject Walk;
    [SerializeField] private GameObject Jump;
    private bool Walked = false;
    private bool ActiveText = false;

    public Health D;
    // Start is called before the first frame update
    void Start()
    {
        Walk.SetActive(true);
        Jump.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            Walk.SetActive(false);
            Walked = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump.SetActive(false);
            ActiveText = true;
        }
        if (Walked && !ActiveText)
        {
            Jump.SetActive(true);
        }
        if(D.Died)
        {
            Jump.SetActive(false);
        }
    }
}
