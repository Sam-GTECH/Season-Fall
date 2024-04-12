using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    GameObject player;

    float speed = 0f;
    float speedVelocity = 0f;

    public float speedIncrease = 0.4f;
    public float speedMaxIncrease = 13.5f;

    public float speedDecrease = 3.5f;
    public float speedDecreaseAir = 0.2f;

    public float maxJump = 12f;

    public float gravity = 3.3f;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    int dir = 0;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("main");
    }

    void Update()
    {
        if (isDashing){
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            Debug.Log("??");
            StartCoroutine(Dash());
            return;
        }

        Vector2 currVelocity = new(0, player.GetComponent<Rigidbody2D>().velocity.y);

        if (player.GetComponent<Rigidbody2D>().gravityScale != gravity)
            player.GetComponent<Rigidbody2D>().gravityScale = gravity;

        speed = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            dir = -1;
            speedVelocity = Mathf.Clamp01(speedVelocity + speedIncrease);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
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

        player.GetComponent<Rigidbody2D>().velocity = currVelocity;
    }

    private IEnumerator Dash()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        TrailRenderer tr = GetComponent<TrailRenderer>();

        bool ping = false;
        player.GetComponent<Base>().deleteLastElement = () => { ping = true; };
        canDash = false;
        isDashing = true; 
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        Debug.Log("we unlock");
        canDash = true;
        if(ping) { Destroy(player.GetComponent<Fire>()); }
        else { player.GetComponent<Base>().deleteLastElement = () => { Destroy(player.GetComponent<Fire>()); }; }
    }
}
