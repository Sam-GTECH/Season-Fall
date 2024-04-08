using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : Element
{
    public override void move()
    {
        Rigidbody2D body = GameObject.FindGameObjectWithTag("main").GetComponent<Rigidbody2D>();
        Vector2 currentVelocity = new Vector2(0, body.velocity.y);

        if (Input.GetKey(KeyCode.A))
        {
            currentVelocity.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            currentVelocity.x += 1;
        }

        body.velocity = currentVelocity;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(new Vector2(0, 1) * 1000);
        }
    }
}
