using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    float speed = 0f;
    float speedVelocity = 0f;

    public float speedIncrease = 0.4f;
    public float speedMaxIncrease = 13.5f;

    public float speedDecreaseNormal = 2.5f;
    public float speedDecreaseAir = 0.2f;
    public float speedDecreaseIce = 0.5f;

    public float maxJump = 12f;

    int dir = 0;

    public int hp = 10;
    int damage = 1;

    Tuple<float, float> last_pos;

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

    public feetManager feet;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(speed + " | ");
        Vector2 currVelocity = new(0, GetComponent<Rigidbody2D>().velocity.y);

        speed = 0f;
        if (Input.GetKey(KeyCode.LeftArrow) )
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            dir = -1;
            speedVelocity = Mathf.Clamp01(speedVelocity+speedIncrease);
        }
        else if (Input.GetKey(KeyCode.RightArrow) )
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
            dir = 1;
            speedVelocity = Mathf.Clamp01(speedVelocity + speedIncrease);
        }
        else
        {
            speed = (speedMaxIncrease * speedVelocity) * dir;
            if ((dir == 1 && speed > 0f || dir == -1 && speed < 0f))
            {
                if (feet.isGrounded)
                {
                    speedVelocity -= speedDecreaseNormal * Time.deltaTime;
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
        speed = (speedMaxIncrease * speedVelocity)*dir;
        currVelocity.x = speed;

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
