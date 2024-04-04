using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Neutral : Element
{
    public override void move()
    {
        Rigidbody2D body = GameObject.FindGameObjectWithTag("main").GetComponent<Rigidbody2D>();
        Vector2 currentVelocity = new Vector2(0, body.velocity.y);

        if (Input.GetKey(KeyCode.A))
        {
            currentVelocity.x -= 10;
        }
        if (Input.GetKey(KeyCode.D))
        {
            currentVelocity.x += 10;
        }

        body.velocity = currentVelocity;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(new Vector2(0, 1000));
        }
    }
}
