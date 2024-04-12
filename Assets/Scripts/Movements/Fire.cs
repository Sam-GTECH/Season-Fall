using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private Animator animator;
    private TrailRenderer tr;

    private float speed = 0f;
    private float speedVelocity = 0f;
    private int dir = 0;

    public float speedIncrease = 0.4f;
    public float speedMaxIncrease = 13.5f;
    public float speedDecrease = 3.5f;
    public float speedDecreaseAir = 0.2f;
    public float maxJump = 12f;
    public float gravity = 3.3f;

    private bool canDash = true;
    private bool isDashing;
    public float dashingPower = 24f;
    public float dashingTime = 0.4f;
    public float dashingCooldown = 1f;


    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("main");
        rb = player.GetComponent<Rigidbody2D>();
        tr = player.GetComponent<TrailRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDashing)
            return;

        else if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            Debug.Log("??");
            StartCoroutine(Dash());
            return;
        }

        Vector2 currVelocity = rb.velocity;

        if (rb.gravityScale != gravity)
            rb.gravityScale = gravity;

        speed = 0f;
        if (!isDashing && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            dir = Input.GetKey(KeyCode.LeftArrow) ? -1 : 1;
            speedVelocity = Mathf.Clamp01(speedVelocity + speedIncrease);
            player.transform.localScale = new Vector2(dir * Mathf.Abs(player.transform.localScale.x), player.transform.localScale.y);
            Debug.Log("Moving direction: " + (dir == -1 ? "left" : "right"));
        }
        
        else
        {
            if (dir != 0)
            {
                if (player.GetComponent<feetManager>().isGrounded)
                {
                    speedVelocity -= speedDecrease * Time.deltaTime;
                }
                else
                {
                    speedVelocity -= speedDecreaseAir * Time.deltaTime;
                }
                speedVelocity = Mathf.Clamp01(speedVelocity);
            }
            dir = 0;
        }

        speed = (speedMaxIncrease * speedVelocity) * dir;
        currVelocity.x = speed;

        if (!isDashing && Input.GetKeyDown(KeyCode.UpArrow) && player.GetComponent<feetManager>().isGrounded)
        {
            currVelocity.y = maxJump;
        }

        rb.velocity = currVelocity;

        animator.SetBool("isRunning", dir != 0);
    }

    private IEnumerator Dash()
    {

        Debug.Log("Dashing in direction: " + (dir == -1 ? "left" : "right"));
        canDash = false;
        isDashing = true;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(dashingPower * dir, 0f);
        tr.emitting = true;

        yield return new WaitForSeconds(dashingTime);

        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);

        canDash = true;
    }
}