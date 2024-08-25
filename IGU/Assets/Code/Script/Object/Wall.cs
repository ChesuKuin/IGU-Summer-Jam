using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject AnotherText;
    // Start is called before the first frame update
    void Start()
    {
        AnotherText.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            AnotherText.SetActive(true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            AnotherText.SetActive(false);
        }
    }
}