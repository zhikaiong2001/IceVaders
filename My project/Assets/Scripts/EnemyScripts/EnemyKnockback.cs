using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    [Header("Knockback")]
    [SerializeField] private float KBForceHor;
    [SerializeField] private float KBForceVer;
    [SerializeField] private float KBTime;
    private bool fromRight;
    public bool isStunned { get; private set; }

    private Rigidbody2D rb;
    private EnemyMovement enemyMovement;
    private EnemyHealth enemyHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    public void knockCheck(bool rightSide)
    {
        fromRight = rightSide;
        if (isStunned)
        {
            return;
        }
        else if (KBTime == 0)
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
        dirCheck();

        isStunned = true;
        enemyMovement.disableMovement();
        if (fromRight)
        {
            rb.velocity = new Vector2(-KBForceHor, KBForceVer);

            if (!enemyMovement.isFacingRight)
            {
                flip();
            }
        }
        else
        {
            rb.velocity = new Vector2(KBForceHor, KBForceVer);

            if (enemyMovement.isFacingRight)
            {
                flip();
            }
        }
        yield return new WaitForSeconds(KBTime);
        isStunned = false;
        rb.velocity = Vector2.zero;
        enemyMovement.enableMovement();
    }

    private void dirCheck()
    {
        if (enemyMovement.rPos > 0f)
        {
            fromRight = false;
        }
    }

    private void flip()
    {
        enemyMovement.isFacingRight = !enemyMovement.isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
