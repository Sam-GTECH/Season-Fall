using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{

    public float movementspeed = 10;
    public float powerJump = 1000;
    Element _currentElement;
    string bombElement = "fire";

    private void OnEnable()
    {
        _currentElement = new Fire();
        Debug.Log("HEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEY");
    }

    // Update is called once per frame
    private void Update()
    {
        _currentElement.move();
        if (Input.GetKeyDown(KeyCode.C)) { changeBombElem(); }
    }
     
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GroundElem"))
        {
            _currentElement = new Ground();
        }
        else if (other.CompareTag("WindElem"))
        {
            _currentElement = new Air();
        }
        else if (other.CompareTag("FireElem"))
        {
            _currentElement = new Fire();
        }
        else if (other.CompareTag("IceElem"))
        {
            _currentElement = new Ice();
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("GroundElem"))
        {
            _currentElement = new Neutral();
        }
        else if (other.CompareTag("WindElem"))
        {
            _currentElement = new Neutral();
        }
        else if (other.CompareTag("FireElem"))
        {
            _currentElement = new Neutral();
        }
        else if (other.CompareTag("IceElem"))
        {
            _currentElement = new Neutral();
        }
    }

    public virtual string getElem()
    {
        return bombElement;
    }

    private void changeBombElem()
    {
        switch (bombElement)
        {
            case "ice":
                bombElement = "fire";
                break;
            case "fire":
                bombElement = "ground";
                break;
            case "ground":
                bombElement = "air";
                break;
            case "air":
                bombElement = "ice";
                break;
            default:
                return;
        }
    }
}