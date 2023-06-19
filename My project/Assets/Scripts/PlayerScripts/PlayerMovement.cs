using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerData;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System;

public class PlayerMovement : MonoBehaviour
{
    // Components
    private Rigidbody2D rb; 
    private BoxCollider2D bc;
    private Animator anim;

    // Basic Movement
    public bool canMove { get; private set; } = true;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [HideInInspector] public bool isFacingRight = true;
    [SerializeField] private float jumpForce = 14f;
    private float jumpScale;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource jumpSoundEffect;

    // Animation State
    private string currentState;
    private enum MovementState { idle, running, jumping, falling };

    // Add-Ons
    private PlayerAttack playerAttack;
    private Dash dash;
    private WallCling wallClling;

    private void Start()
    {
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        dash = GetComponent<Dash>();
        wallClling = GetComponent<WallCling>();
        transform.position = Player.startingPosition.initialValue;
    }


    private void Update()
    {
        if (!canMove)
        {
            return;
        }

        if (PauseMenu.GameIsPaused)
        {
            return;
        }

        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }

        flipCheck();

        dash.dashCheck();
        playerAttack.attackCheck();
        wallClling.wallClingCheck();

        updateAnimationState();
    }

    private void updateAnimationState()
    {
        MovementState state;
        if (dirX > 0.5f)
        {
            state = MovementState.running;
        }
        else if (dirX < -0.5f)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }
        anim.SetInteger("state", (int)state);
    }

    private void flipCheck()
    {
        if (isFacingRight && dirX < 0f || !isFacingRight && dirX > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }


    public bool isGrounded()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }



    // Universal Movement Controls
    public void enableMovement()
    {
        canMove = true;
    }

    public void disableMovement()
    {
        canMove = false;
    }
}
