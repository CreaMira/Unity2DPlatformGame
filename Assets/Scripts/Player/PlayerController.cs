using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Transform keyFollowPoint;

    /* Component */
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    [SerializeField] private TrailRenderer tr;

    [SerializeField] private LayerMask groundLayer; //check if player is on ground
    [SerializeField] private Transform groundCheck; //check if player is on ground

    //
    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    /* Movement */
    private float dirX = 0f; // -1 ~ 1
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 4f;
    private bool isFacingRight = true;

    /* Dash */
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCD = 1f;

    /* Jump */
    [SerializeField] private bool onGround = false;

    private void Update()
    {
        if(isDashing)
        {
            return;
        }

        /* Horizontal Movement */
        dirX = Input.GetAxisRaw("Horizontal");
        Flip();

        /* Jump */
        Jump();

        /* Dash */
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            Debug.Log(" -- Dash --");
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if(isDashing)
        {
            return;
        }

        /* Horizontal Movement */
        rb.velocity = new Vector2( dirX * moveSpeed, rb.velocity.y );

        /** Animation **/
        /* Animation - Running */
        UpdateAnimation(dirX);
    }


    /** Jump **/
    private void Jump()
    {
        if ( Input.GetButtonDown("Jump") && IsGrounded() )
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2( rb.velocity.x, jumpForce );
        }

        if( Input.GetButtonUp("Jump") && rb.velocity.y > 0f )
        {
            rb.velocity = new Vector2( rb.velocity.x, rb.velocity.y * 0.5f );
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }


    /** Animation **/
    /*
        idel    = 0
        running = 1
        jumping = 2
        falling = 3
    */
    private enum MovementState{ idel, running, jumping, falling};
    private MovementState state = MovementState.idel;

    private void UpdateAnimation(float dirX)
    {

        /* dirX - running */
        if (dirX == 0f) //idel
        {
            state = MovementState.idel;
        }
        else if (dirX > 0f) //running right
        {
            state = MovementState.running;
        }
        else if (dirX < 0f) //running left
        {
            state = MovementState.running;
        }

        /* dirY - Jumping */
        if(rb.velocity.y > .1f && !onGround)
        {
            state = MovementState.jumping;
        }
        else if(rb.velocity.y < -.2f && !onGround)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    
    private void Flip()
    {
        if ( (isFacingRight && dirX < 0f) || (!isFacingRight && dirX > 0f) )
        {
            isFacingRight = !isFacingRight;

            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

    }

    private IEnumerator Dash()
    {
        //start to dashing
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        //dashing
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;

        //stop dashing
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;

        //cool down
        yield return new WaitForSeconds(dashingCD);
        canDash = true;
    }
}
