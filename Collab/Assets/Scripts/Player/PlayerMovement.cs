using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private CircleCollider2D coll;
    private SpriteRenderer sprite;
    float directionX = 0f;


    [SerializeField] private LayerMask jumpableGround;


    [SerializeField] private float moveSpeed;

    Vector2 vecGravity;

    [Header("Jump Syatem")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpStartTime;
    [SerializeField] private float fallStartTime;
    private float jumpTime;
    private float fallTime;
    private float jumpCounter;
    [SerializeField] private float jumpMulitplier;
    [SerializeField] private float fallMultiplier;

    private bool isJumping;

    private enum MovementState { idle, running, falling, jumping };
    



    // Start is called before the first frame update
    private void Start()
    {

        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(directionX * moveSpeed, rb.velocity.y);

  


        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            isJumping = true;
            jumpTime = jumpStartTime;
            fallTime = fallStartTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        //block of code for "mario" jump
        if (Input.GetButton("Jump") && isJumping == true) //check for holding space key
        {
            jumpCounter += Time.deltaTime;
            float t = jumpCounter / jumpTime;
            float currentJumpM = jumpMulitplier;
            if (jumpTime > 0)
            {
                if (t < 0.5f) //if half the jump time is up, the character moves upward slower.
                {
                    currentJumpM = jumpMulitplier * (1 - t);
                }
                //rb.velocity = new Vector2(rb.velocity.x, jumpMulitplier);
                rb.velocity += vecGravity * currentJumpM * Time.deltaTime;
                jumpTime -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump")) //if the user isn't holding space, then isJumping is false, no more increase in jumpforce
        {
            isJumping = false;
        }


        if(rb.velocity.y < 0) // faster falling
        {
            if (fallTime > 0)
            {
                rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
                fallTime -= Time.deltaTime;
            }
        }
  
        UpdateAnimationState();

    }

    private void UpdateAnimationState()
    {

        MovementState state;

        if (directionX > 0f)
        {
            sprite.flipX = false;
            state = MovementState.running;
        }
        else if (directionX < 0f)
        {
            sprite.flipX = true;
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if(rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }

        else if(rb.velocity.y < -1.5f)
        {
            state = MovementState.falling;
        }

        animator.SetInteger("state", (int)state);
    }

    private bool isGrounded()
    {
       return Physics2D.CircleCast(coll.bounds.center, 0.6925139f, Vector2.down, .1f, jumpableGround);

    }
}
