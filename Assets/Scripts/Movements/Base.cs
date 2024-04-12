using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    GameObject player;
    public Action deleteLastElement;
    string bombElement;

    // Start is called before the first frame update
    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("main");
        deleteLastElement = () => { };
        setElement(GameObject.Find("Grid").GetComponent<GridElement>().getElement());
        bombElement = "fire";
    }

    // Update is called once per frame
    void Update()
    {

    }

    void setElement(string elem)
    {
        switch(elem) {
        case "neutral":
                deleteLastElement();
                player.AddComponent<Neutral>();
                deleteLastElement = () => { Destroy(player.GetComponent<Neutral>()); };
            break;
        case "ice":
                deleteLastElement();
                player.AddComponent<Ice>();
                deleteLastElement = () => { Destroy(player.GetComponent<Ice>()); };
                break;
        case "fire":
                deleteLastElement();
                player.AddComponent<Fire>();
                deleteLastElement = () => { Destroy(player.GetComponent<Fire>()); };
                break;
        case "ground":
                deleteLastElement();
                player.AddComponent<Ground>();
                deleteLastElement = () => { Destroy(player.GetComponent<Ground>()); };
                break;
        case "air":
                deleteLastElement();
                player.AddComponent<Air>();
                deleteLastElement = () => { Destroy(player.GetComponent<Air>()); };
                break;
        default:
            return;
        } 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GroundElem"))
        {
            setElement("ground");
        }
        else if (other.CompareTag("WindElem"))
        {
            setElement("air");
        }
        else if (other.CompareTag("FireElem"))
        {
            setElement("fire");
        }
        else if (other.CompareTag("IceElem"))
        {
            setElement("ice");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("GroundElem") || other.CompareTag("WindElem") || other.CompareTag("FireElem") || other.CompareTag("IceElem"))
        {
            setElement("neutral");
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
