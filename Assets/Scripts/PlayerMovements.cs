using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    public int hp = 10;
    private int damage = 1;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 700f;
    private float dashingTime = 1f;
    private float dashingCooldown = 1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }
        //Trop beau le prof ^^
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        print(Input.GetKeyDown(KeyCode.LeftShift) + " " + canDash);
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        Flip();
        //Pouah les muscles du prof mama comment il est trop muscl�����
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    private void FixedUpdate()
    {
        if (isDashing == false)
        {
            //    return;
            print("Moove");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        else
        {
            Dashing();
        }

    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    //
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        //rb.velocity = new Vector2( dashingPower * Time.deltaTime, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        print("Dash");
    }

    public void Dashing()
    {
        rb.velocity = new Vector2(dashingPower * Time.deltaTime, 0f);
    }

    public void TakeDamage()
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        StartCoroutine(WaitInvisibility());
    }

    private IEnumerator WaitInvisibility()
    {
        // Add invisibility logic here
        yield return new WaitForSeconds(1);
    }
}
