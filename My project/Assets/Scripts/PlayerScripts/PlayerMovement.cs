using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private bool jumpUnlocked = false;
    [SerializeField] private bool dashUnlocked = false;
    [SerializeField] private bool wallJumpUnlocked = false;


    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    private bool isFacingRight = true;
    private float horizontal;

    private string currentState;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(20f, 30f);

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    [Header ("Knockback")]
    [SerializeField] private float KBForceHor;
    [SerializeField] private float KBForceVer;
    [SerializeField] private float KBCounter;
    [SerializeField] private float KBTotalTime;
    [SerializeField] private float StunDuration;
    [SerializeField] private float StunTotal;
    [SerializeField] private bool KnockFromRight;
    public bool isStunned { get; private set; }

    //Scene transition starting position
    public VectorValue startingPosition;

    //Animation states
    private enum MovementState { idle, running, jumping, falling };

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isDashing)
        {
            return;
        }


        // Knockback Logic
        if (KBCounter <= .01f && StunDuration <= .01f)
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
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            if (!dashUnlocked)
            {
                return;
            }
            StartCoroutine(Dash());
        }
        if (wallJumpUnlocked)
        {
            WallSlide();
            WallJump();
        }
        UpdateAnimationState();

        if (isWallJumping == false)
        {
            Flip();
        }
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

    private IEnumerator Dash()
    {
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
        canDash = true;
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
            wallJumpingDirection = transform.localScale.x;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "DashSkillOrb")
        {
            dashUnlocked = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.name == "WallJumpSkillOrb")
        {
            wallJumpUnlocked = true;
            Destroy(collision.gameObject);
        }
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
