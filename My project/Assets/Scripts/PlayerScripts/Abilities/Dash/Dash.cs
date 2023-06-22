using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private Rigidbody2D rb;

    public bool isDashing { get; private set; }
    private bool canDash;
    private bool dashUnlocked;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;
    [SerializeField] private TrailRenderer tr;

    private void Start()
    {
        isDashing = false;
        canDash = true;
        rb = GetComponent<Rigidbody2D>();
    }

    public void dashCheck()
    {
        if (playerMovement.isGrounded())
        {
            canDash = true;
        }

        dashUnlocked = Player.unlocked[(int)Player.Abilities.dash];

        if (Input.GetKeyDown(KeyCode.Z) && canDash && dashUnlocked)
        {
            StartCoroutine(startDash());
        }
    }

    private IEnumerator startDash()
    {
        canDash = false;
        isDashing = true;
        playerMovement.disableMovement();
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        playerMovement.enableMovement();
        yield return new WaitForSeconds(dashingCooldown);
    }
}
