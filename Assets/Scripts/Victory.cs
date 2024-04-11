using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System;

public class Victory : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string title = SceneManager.GetActiveScene().name;
        string[] subs = title.Split(" ");
        bool succ = int.TryParse(subs[1], out int num);
        if (!succ)
            throw new ArgumentException($"Something went wrong with the name of the current scene. ({title})");
        Debug.Log(subs[0] + " " + (num + 1));
        SceneManager.LoadScene(subs[0] + " " + (num+1));
    }
}
