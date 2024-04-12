using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class OldGround : Element
{
    GameObject player = GameObject.FindGameObjectWithTag("main");

    float speed = 0f;
    float speedVelocity = 0f;

    public float speedIncrease = 0.4f;
    public float speedMaxIncrease = 5.5f;

    public float speedDecrease = 3.5f;
    public float speedDecreaseAir = 0.5f;
    //public float speedDecreaseIce = 0.5f; 

    public float maxJump = 10f;

    public float gravity = 6.3f;

    int dir = 0;

    //public int hp = 10;
    //int damage = 1;
    public override void move()
    {
        Debug.Log("Ground");
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
        player.GetComponent<Rigidbody2D>().velocity = currVelocity;
    }

    public override string getElem()
    {
        return "ground";
    }

}
