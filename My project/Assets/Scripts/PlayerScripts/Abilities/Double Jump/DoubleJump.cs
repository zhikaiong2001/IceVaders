using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    [Header("Attributes")]
    public float coyoteTime;
    public float coyoteTimeCounter;
    public float secondJumpPower;
    private bool canDoubleJump;
    public AudioSource secondJumpSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void doubleJumpCheck()
    {
        if (playerMovement.isGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            canDoubleJump = true;
        }
        else if (coyoteTimeCounter < 0f && !playerMovement.jumped)
        {
            canDoubleJump = false;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (!playerMovement.isGrounded() && Input.GetButtonDown("Jump") && canDoubleJump && Player.unlockCheck((int)Player.Abilities.doubleJump))
        {
            secondJump();
        }
    }

    public void secondJump()
    {
        playerMovement.isFirstJump = false;
        rb.velocity = new Vector2(rb.velocity.x, secondJumpPower);
        canDoubleJump = false;
        //secondJumpSoundEffect.Play();
    }
}
