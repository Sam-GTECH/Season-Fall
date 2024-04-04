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
    public float maxJump = 10f;

    public GameObject scale;
    public feetManager feet;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(speed + " | " +  speedIncrease);
        Vector2 currVelocity = new(0, GetComponent<Rigidbody2D>().velocity.y);

        if (Input.GetKey(KeyCode.LeftArrow)) {
            speed -= speedIncrease;
            //currVelocity.x -= speed;
        } else
        {
            speed = Mathf.Clamp(speed - speedIncrease, 0f, speedMaxIncrease);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            speed += speedIncrease;
            //currVelocity.x += speed;
        } else {
            speed = Mathf.Clamp(speed-speedIncrease, -speedMaxIncrease, 0f);
        }

        if (speed != 0f)
        {
            if (speed < 0f)
                speed = Mathf.Clamp(speed, -speedMaxIncrease, 0f);
            else if (speed > 0f)
                speed = Mathf.Clamp(speed, 0f, speedMaxIncrease);
            currVelocity.x += speed;

            GetComponent<Rigidbody2D>().velocity = currVelocity;
        }

        if (Input.GetKey(KeyCode.Space) && feet.isGrounded) {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpIncrease));
        }
    }
}
