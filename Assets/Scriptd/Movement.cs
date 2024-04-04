using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D body;
    public float speed;
    public float powerJump;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentVelocity = new Vector2(0, body.velocity.y);

        if (Input.GetKey(KeyCode.A))
        {
            currentVelocity.x -= speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            currentVelocity.x += speed;
        }

        body.velocity = currentVelocity;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(new Vector2(0, powerJump));
            Debug.Log("Cécédillea saute ou quoi laa");
            Debug.Log(body.velocity);
        }
    }
}
