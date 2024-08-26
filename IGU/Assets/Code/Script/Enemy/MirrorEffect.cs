using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorEffect : MonoBehaviour
{
    public GameObject badGuyPrefab;
    public GameObject goodGuy;// Drag the "Bad Guy" prefab here in the Inspector
    public GameObject Exit1;

    void Start()
    {
        Exit1.SetActive(true);
}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Pow")
        {
            ShowMirror();
        }
    }
    // This function should be called when the mirror is shown to the enemy
    public void ShowMirror()
    {
        // Get the position and rotation of the "Good Guy"
        Vector3 position = goodGuy.transform.position;
        Quaternion rotation = goodGuy.transform.rotation;

        // Destroy the "Good Guy"
        Destroy(goodGuy);
        Exit1.SetActive(false);

        // Instantiate the "Bad Guy" at the same position and rotation
        Instantiate(badGuyPrefab, position, rotation);
    }
}
