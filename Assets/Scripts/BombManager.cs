using System;
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
                break;
            case "fire":
                currentZone = FireZone;
                currentElem = "fire";
                break;
            case "air":
                currentZone = AirZone;
                currentElem = "air";
                break;
            case "ice":
                currentZone = IceZone;
                currentElem = "ice";
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
            Instantiate(currentZone, transform.position, transform.rotation);
            foreground.GetComponent<ChangeTile>().HandleTilePlacing(GetComponent<Transform>().position, currentElem);
            background.GetComponent<ChangeTile>().HandleTilePlacing(GetComponent<Transform>().position, currentElem);
            //background.GetComponent<ChangeTile>().HandleTilePlacing(GetComponent<Transform>().position, "neutral");
            Destroy(this.gameObject);
        }
    }
}