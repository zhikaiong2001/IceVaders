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
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    // Basic Movement
    private bool canMove = true;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    private bool isFacingRight = true;
    private float horizontal;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource jumpSoundEffect;

    // Animation State
    private string currentState;
    private enum MovementState { idle, running, jumping, falling };

    public Dash dash;


    // Wall Slide
    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    [SerializeField] private Vector2 wallJumpingPower = new Vector2(16f, 32f);
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;


    // Knockback
    [Header ("Knockback")]
    [SerializeField] private float KBForceHor;
    [SerializeField] private float KBForceVer;
    [SerializeField] private float KBCounter;
    [SerializeField] private float KBTotalTime;
    [SerializeField] private float StunDuration;
    [SerializeField] private float StunTotal;
    [SerializeField] private bool KnockFromRight;
    public bool isStunned { get; private set; }


    private void Start()
    {
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        transform.position = Player.startingPosition.initialValue;
    }

    private void Update()
    {
        if (!canMove)
        {
            Debug.Log("faggot");
            return;
        }

        if (PauseMenu.GameIsPaused)
        {
            return;
        }

        // Knockback Logic
        if (KBCounter <= .01f && StunDuration <= .01f && !isWallJumping)
        {
            isStunned = false;
            dirX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        }
        else if (KBCounter > .01f)
        {
            isStunned = true;
            if (KnockFromRight)
            {
                rb.velocity = new Vector2(-KBForceHor, KBForceVer);

                if (!isFacingRight)
                {
                    isFacingRight = !isFacingRight;
                    Vector3 localScale = transform.localScale;
                    localScale.x *= -1f;
                    transform.localScale = localScale;
                }
            }
            else
            {
                rb.velocity = new Vector2(KBForceHor, KBForceVer);

                if (isFacingRight)
                {
                    isFacingRight = !isFacingRight;
                    Vector3 localScale = transform.localScale;
                    localScale.x *= -1f;
                    transform.localScale = localScale;
                }
            }

            KBCounter -= Time.deltaTime;
            StunDuration -= Time.deltaTime;
        }
        else
        {
            isStunned = true;
            StunDuration -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (PlayerStatic.canWallCling)
        {
            WallSlide();
            WallJump();
        }
        UpdateAnimationState();

        if (isWallJumping == false)
        {
            Flip();
            
        }

        dash.dashCheck();
    }

    private void UpdateAnimationState()
    {
        MovementState state;
        if (dirX > 0.1f)
        {
            state = MovementState.running;
        }
        else if (dirX < -0.1f)
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

    private void Flip()
    {
        if (isFacingRight && dirX < 0f || !isFacingRight && dirX > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }


    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && dirX != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;
            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }

    }

    private void StopWallJumping()
    {
        isWallJumping = false;
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


    // Knocback Methods
    public void setKBCounter(float time)
    {
        KBCounter = time;
    }

    public float getKBTotalTime()
    {
        return KBTotalTime;
    }

    public void setStunCounter(float time)
    {
        StunDuration = time;
    }

    public float getStunTotal()
    {
        return StunTotal;
    }

    public void setKBRight(bool right)
    {
        KnockFromRight = right;
    }
}
