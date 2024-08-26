using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TtoPickUp : MonoBehaviour
{
    public GameObject TtoReflect;
    // Start is called before the first frame update
    void Start()
    {
        TtoReflect.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            TtoReflect.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            TtoReflect.SetActive(false);
        }
    }
}
