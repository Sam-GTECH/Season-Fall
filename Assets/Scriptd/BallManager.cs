using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public float speed = 0f;
    public float speedIncrease = 0.4f;
    public float speedMaxIncrease = 13.5f;

    public float currJump = 0f;
    public float jumpIncrease = 3f;
    public float maxJump = 12f;

    public int dir = 0;

    public int hp = 10;
    int damage = 1;

    public void takeDamage()
    {
        hp = hp - damage;
        if (hp < 0)
        {
            Destroy(gameObject);
        }
        StartCoroutine(waitInvisibility());
    }

    public IEnumerator waitInvisibility()
    {
        yield return new WaitForSeconds(1);
    }

    public GameObject scale;
    public feetManager feet;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(speed + " | " + speedIncrease);
        Vector2 currVelocity = new(0, GetComponent<Rigidbody2D>().velocity.y);

        speed = 0f;
        if (Input.GetKey(KeyCode.LeftArrow) )
        {
            speed = -speedMaxIncrease;
        }
        else if (Input.GetKey(KeyCode.RightArrow) )
        {
            speed = speedMaxIncrease;
        }

        if (Input.GetKey(KeyCode.UpArrow) )
        {
            if (feet.isGrounded)
            {
                currVelocity.y = maxJump;
            }
        }

        currVelocity.x = speed;
        GetComponent<Rigidbody2D>().velocity = currVelocity;
    }
}
