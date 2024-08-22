using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPickUp : MonoBehaviour
{
    public GameObject Text;

    private bool pickUpAllowed;

    void Start()
    {
        Text.SetActive(false);
        pickUpAllowed = false;
    }

    private void Update()
    {
        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E))
            PickUp();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Text.SetActive(true);
            pickUpAllowed = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Text.SetActive(false);
        pickUpAllowed = false;
    }

    private void PickUp()
    {
        Destroy(gameObject);
    }
}
