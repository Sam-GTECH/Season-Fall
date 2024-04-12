using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class OldFire : Element
{
    GameObject player;

    float speed = 0f;
    float speedVelocity = 0f;

    public float speedIncrease = 0.4f;
    public float speedMaxIncrease = 13.5f;

    public float speedDecrease = 3.5f;
    public float speedDecreaseAir = 0.2f;
    //public float speedDecreaseIce = 0.5f; 

    public float maxJump = 12f;

    public float gravity = 3.3f;

    public bool dash = false;
    public int dash_dir = 0;
    public float dash_timer = 0;
    public float dash_limit = 1f;

    int dir = 0;
    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("main");
    }

    public override void move()
    {
        //Debug.Log("Fire Fire Light The Fire");
        Vector2 currVelocity = new(0, player.GetComponent<Rigidbody2D>().velocity.y);

        if (player.GetComponent<Rigidbody2D>().gravityScale != gravity)
            player.GetComponent<Rigidbody2D>().gravityScale = gravity;

        if (Input.GetKeyDown(KeyCode.Space) && !dash)
        {
            dash = true;
            dash_dir = 1;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                dash_dir = -1;
            }
        }

        if (!dash)
        {
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
                Debug.Log(player.GetComponent<feetManager>().isGrounded);
                if (player.GetComponent<feetManager>().isGrounded)
                {
                    currVelocity.y = maxJump;
                }
            }

            currVelocity.x = speed;
            player.GetComponent<Rigidbody2D>().velocity = currVelocity;
        }
        else
        {
            Debug.Log(dash_timer + " | " + dash_limit);
            dash_timer += 1 * Time.deltaTime;
            if (dash_timer >= dash_limit)
            {
                dash_timer = 0;
                dash = false;
                dir = dash_dir;
                dash_dir = 0;
                player.GetComponent<Rigidbody2D>().gravityScale = gravity;
                return;
            }
            currVelocity.y = 0f;
            currVelocity.x = 15f * dash_dir;
            player.GetComponent<Rigidbody2D>().gravityScale = 0f;
            player.GetComponent<Rigidbody2D>().velocity = currVelocity;
        }
    }

    public override string getElem()
    {
        return "fire";
    }
}
