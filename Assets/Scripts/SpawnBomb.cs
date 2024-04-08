using UnityEngine;

public class SpawnBomb : MonoBehaviour
{
    public GameObject bomb;
    public GameObject player;
    public float offset;


    // Start is called before the first frame update
    private void Start()
    {
    }

    //créer une bombe
    private void CreateBomb()
    {
        Debug.Log("CreateBomb");

        //Get player
        player = GameObject.FindWithTag("Player");

        //Définir la position de la bombe
        //transform.position = new Vector3(player.transform.position.x + offset, player.transform.position.y, player.transform.position.z);

        //Créer un objet
        Instantiate(bomb, transform.position + Vector3.right * (offset * Random.Range(1.0f, 1.1f)), transform.rotation);

        //Debug log of bomb coordinates
        Debug.Log("Bomb coordinates: " + transform.position);
    }


    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            CreateBomb();
        }
    }
}