using UnityEngine;

public class Neutral : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private Animator animator;

    private float speed = 0f;
    private float speedVelocity = 0f;
    private int dir = 0;

    public float speedIncrease = 0.4f;
    public float speedMaxIncrease = 13.5f;
    public float speedDecrease = 3.5f;
    public float speedDecreaseAir = 0.2f;
    public float maxJump = 12f;
    public float gravity = 3.3f;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("main");
        rb = player.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 currVelocity = rb.velocity;

        if (rb.gravityScale != gravity)
            rb.gravityScale = gravity;

        speed = 0f;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            dir = Input.GetKey(KeyCode.LeftArrow) ? -1 : 1;
            speedVelocity = Mathf.Clamp01(speedVelocity + speedIncrease);
            player.transform.localScale = new Vector2(dir * Mathf.Abs(player.transform.localScale.x), player.transform.localScale.y);
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

        if (Input.GetKeyDown(KeyCode.UpArrow) && player.GetComponent<feetManager>().isGrounded)
        {
            currVelocity.y = maxJump;
        }

        rb.velocity = currVelocity;

        animator.SetBool("isRunning", dir != 0);
    }
}
