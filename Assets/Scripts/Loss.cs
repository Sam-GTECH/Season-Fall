using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Loss : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Loss for: " + collision.gameObject.tag);
        if (collision.gameObject.tag != "main")
            return;
        string title = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(title);
    }
}
