using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feur : MonoBehaviour
{

    public float movementspeed = 10;
    public float powerJump = 1000;
    Element _currentElement;

    private void OnEnable()
    {
        _currentElement = new Ground();
        Debug.Log("HEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEY");
    }

    // Update is called once per frame
    private void Update()
    {
        _currentElement.move();
    }
     
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

/*    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ground"))
        {
            _currentElement = new Ground();
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ground"))
        {
            _currentElement = new Neutral();
        }
    }*/
}