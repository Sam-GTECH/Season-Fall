using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombManager : MonoBehaviour
{
    public float radius;
    public GameObject FireZone;
    public GameObject IceZone;
    public GameObject AirZone;
    public GameObject GroundZone;
    public Sprite FireBomb;
    public Sprite IceBomb;
    public Sprite AirBomb;
    public Sprite GroundBomb;
    private string currentElem;
    private GameObject currentZone;
    private GameObject player;
    private Tilemap foreground;
    private Tilemap background;
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.Find("Player");
        foreground = GameObject.Find("front").GetComponent<Tilemap>();
        background = GameObject.Find("back").GetComponent<Tilemap>();
        switch (player.GetComponent<Base>().getElem())
        {
            case "ground":
                currentZone = GroundZone;
                currentElem = "ground";
                GetComponent<SpriteRenderer>().sprite = GroundBomb;
                break;
            case "fire":
                currentZone = FireZone;
                currentElem = "fire";
                GetComponent<SpriteRenderer>().sprite = FireBomb;
                break;
            case "air":
                currentZone = AirZone;
                currentElem = "air";
                GetComponent<SpriteRenderer>().sprite = AirBomb;
                break;
            case "ice":
                currentZone = IceZone;
                currentElem = "ice";
                GetComponent<SpriteRenderer>().sprite = IceBomb;
                break;
        }
    }

    private void SetRadius(float radius)
    {
        Debug.Log("SetRadius");

        //Get all colliders in a circle
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        //For each collider in the circle
        foreach (Collider2D collider in colliders)
        {
            //If the collider is a player
            if (collider.gameObject.tag == "Player")
            {
                //Deal damage to the player
                DealDamage(10);
            }
        }
    }

    private void DealDamage(int damage)
    {
        Debug.Log("DealDamage");

        //Get player
        player = GameObject.FindWithTag("Player");

        //Inflict damage to player
        player.GetComponent<LifeSystem>().TakeDamage(damage);
    }

    private void Throw()
    {
        Debug.Log("Throw");

        //Jeter un objet
        GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * 200);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D");

        //Si la bombe entre en collision avec un objet
        if (collision.gameObject.tag == "Player")
        {
            DealDamage(10);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Throw();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SetRadius(5);
            GameObject instance = Instantiate(currentZone, transform.position, transform.rotation);
            Vector3 pos = GetComponent<Transform>().position;
            ElemDecay.handleExistingZones(() => {
                Destroy(instance);
                foreground.GetComponent<ChangeTile>().HandleTilePlacing(pos, GameObject.Find("Grid").GetComponent<GridElement>().getElement());
                background.GetComponent<ChangeTile>().HandleTilePlacing(pos, GameObject.Find("Grid").GetComponent<GridElement>().getElement());
            });
            foreground.GetComponent<ChangeTile>().HandleTilePlacing(pos, currentElem);
            background.GetComponent<ChangeTile>().HandleTilePlacing(pos, currentElem);            
            Destroy(this.gameObject);
        }
    }
}

static class ElemDecay
{
    static List<Action> activeZones = new List<Action>();
    public static void handleExistingZones(Action newZone)
    {
        activeZones.Add(newZone);
        if (activeZones.Count >= 3)
        {
            activeZones[0]();
            activeZones.RemoveAt(0);
        }
    }
}