using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickedUp : MonoBehaviour
{
    public GameObject MageMirror;
    private bool pickUpAllowed;
    public GameObject Text;
    public GameObject MirrorOnWall;
    public bool pickedUp = false;
    public GameObject MirrorPickUpG;

    void Start()
    {
        pickUpAllowed = false;
        Text.SetActive(false);
        MageMirror.SetActive(false);
    }

    private void Update()
    {
        if (pickUpAllowed == true && Input.GetKeyDown(KeyCode.E))
            PickUp();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && pickedUp == false)
        {
            Text.SetActive(true);
            pickUpAllowed = true;
            Debug.Log("Triggered");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Text.SetActive(false);
        pickUpAllowed = false;
        Debug.Log("Not Triggered");

        
    }

    private void PickUp()
    {
        Destroy(MirrorOnWall);
        pickedUp = true;
        MageMirror.SetActive(true);
        Debug.Log("Picked");
        Destroy(MirrorPickUpG);
    }

   
}
