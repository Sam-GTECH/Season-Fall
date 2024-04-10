using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Air : Element
{
    GameObject player = GameObject.FindGameObjectWithTag("main");

    float speed = 0f;
    float speedVelocity = 0f;

    public float speedIncrease = 0.4f;
    public float speedMaxIncrease = 13.5f;

    public float speedDecrease = 3.5f;
    public float speedDecreaseAir = 0.2f;
    //public float speedDecreaseIce = 0.5f; 

    public float maxJump = 12f;

    public float gravity = 3.3f;

    int dir = 0;

    //public int hp = 10;
    //int damage = 1;
    public override void move()
    {
        Debug.Log("Air");
        Vector2 currVelocity = new(0, player.GetComponent<Rigidbody2D>().velocity.y);

        if (player.GetComponent<Rigidbody2D>().gravityScale != gravity)
            player.GetComponent<Rigidbody2D>().gravityScale = gravity;

        speed = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            player.GetComponent<SpriteRenderer>().color = Color.red;
            dir = -1;
            speedVelocity = Mathf.Clamp01(speedVelocity + speedIncrease);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            player.GetComponent<SpriteRenderer>().color = Color.blue;
            dir = 1;
            speedVelocity = Mathf.Clamp01(speedVelocity + speedIncrease);
        }
        else
        {
            speed = (speedMaxIncrease * speedVelocity) * dir;
            if ((dir == 1 && speed > 0f || dir == -1 && speed < 0f))
            {
                if (player.GetComponent<feetManager>().isGrounded)
                {
                    speedVelocity -= speedDecrease * Time.deltaTime;
                }
                else
                {
                    speedVelocity -= speedDecreaseAir * Time.deltaTime;
                }
                speed = Mathf.Lerp(0f, speedMaxIncrease, speedVelocity);
            }
            else
            {
                speed = 0f;
                speedVelocity = 0f;
                dir = 0;
            }
        }
        speed = (speedMaxIncrease * speedVelocity) * dir;
        currVelocity.x = speed;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (player.GetComponent<feetManager>().isGrounded)
            {
                currVelocity.y = maxJump;
            }
        }

        currVelocity.x = speed;
        currVelocity.y = currVelocity.y + 0.2f;
        player.GetComponent<Rigidbody2D>().velocity = currVelocity;
    }

    public override string getElem()
    {
        return "air";
    }
}
