using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 1000f;
    public bool isGrounded = false;

    private void Move()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 currentVelocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);

        if (Input.GetKey(KeyCode.D))
        {
            currentVelocity.x += speed;
            Debug.Log("D");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            currentVelocity.x -= speed;
            Debug.Log("Q");
        }

        GetComponent<Rigidbody2D>().velocity = currentVelocity;

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0,1) * 1000);
            isGrounded = false;
        }
    }
}