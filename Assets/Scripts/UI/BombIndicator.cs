using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombIndicator : MonoBehaviour
{
    public List<string> types = new List<string> { "fire", "ground", "air", "ice" };


    GameObject player;
    string last_elem;

    public Image current;
    public Image next;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("main");
        last_elem = player.GetComponent<Base>().getElem();

        current.sprite = Resources.Load<Sprite>("bombs/"+last_elem);
        next.sprite = Resources.Load<Sprite>("bombs/" + types[types.IndexOf(last_elem) + 1]);
    }

    // Update is called once per frame
    void Update()
    {
        if (last_elem != player.GetComponent<Base>().getElem())
        {
            last_elem = player.GetComponent<Base>().getElem();

            current.sprite = Resources.Load<Sprite>("bombs/" + last_elem);
            int index = types.IndexOf(last_elem) + 1;
            if (index > types.Count-1)
            {
                index = 0;
            }
            next.sprite = Resources.Load<Sprite>("bombs/" + types[index]);
        }
    }
}
