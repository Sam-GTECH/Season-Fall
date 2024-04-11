using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement : MonoBehaviour
{
    public string levelElement;
    // Start is called before the first frame update

    private void Start()
    {
        GameObject.FindGameObjectWithTag("main").GetComponent<Base>().setElement(levelElement);
    }
    public string getElement()
    {
        return levelElement;
    }
}
