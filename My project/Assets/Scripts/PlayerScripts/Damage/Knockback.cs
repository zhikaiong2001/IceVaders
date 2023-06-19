using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Knockback : MonoBehaviour
{
    [Header("Knockback")]
    [SerializeField] private float KBForceHor;
    [SerializeField] private float KBForceVer;
    [SerializeField] private float KBTime;
    [HideInInspector] public bool fromRight;
    private bool isStunned;

    private Rigidbody2D rb;

    public PlayerMovement playerMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void knockCheck()
    {
        if (Player.isDead || isStunned)
        {
            return;
        }
        else
        {
            StartCoroutine(knock());
        }
    }

    private IEnumerator knock()
    {
        isStunned = true;
        playerMovement.disableMovement();
        if (fromRight)
        {
            rb.velocity = new Vector2(-KBForceHor, KBForceVer);

            if (!playerMovement.isFacingRight)
            {
                flip();
            }
        }
        else
        {
            rb.velocity = new Vector2(KBForceHor, KBForceVer);

            if (playerMovement.isFacingRight)
            {
                flip();
            }
        }
        yield return new WaitForSeconds(KBTime);
        isStunned = false;
        playerMovement.enableMovement();
    }

    private void flip()
    {
        playerMovement.isFacingRight = !playerMovement.isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
