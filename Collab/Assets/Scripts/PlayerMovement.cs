using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    float directionX;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(directionX * 13f, rb.velocity.y);

        

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 22f);
        }
        UpdateAnimationState();

    }

    private void UpdateAnimationState()
    {
        if (directionX > 0f)
        {
            sprite.flipX = false;
            animator.SetBool("Running", true);
        }
        else if (directionX < 0f)
        {
            sprite.flipX = true;
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }
}
