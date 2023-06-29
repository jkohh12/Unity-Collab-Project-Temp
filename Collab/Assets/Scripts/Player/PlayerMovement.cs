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
    [SerializeField] private float jumpForce;

    private enum MovementState { idle, running, falling, jumping };
    



    // Start is called before the first frame update
    private void Start()
    {
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
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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

        else if(rb.velocity.y < -0.1f)
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
