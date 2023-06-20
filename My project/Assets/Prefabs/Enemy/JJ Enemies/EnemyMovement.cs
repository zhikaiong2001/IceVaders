using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Components
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private Animator anim;
    public Transform player;

    // State Control
    public bool canMove { get; private set; }
    public bool alerted { get; private set; }
    public float rPos { get; private set; }
    public float alertDist;
    [HideInInspector] public bool isFacingRight;

    // Animation State
    private string currentState;
    private enum MovementState { idle, alerted };

    // Add-Ons
    private EnemyAttack enemyAttack;
    private EnemyIdling enemyIdling;
    private EnemyAI enemyAI;

    private void Start()
    {
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        enemyIdling = GetComponent<EnemyIdling>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyAI = GetComponent<EnemyAI>();
        isFacingRight = true;
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

        rPos = transform.position.x - player.position.x;

        if (rPos < alertDist)
        {
            alerted = true;
        }

        enemyIdling.idleCheck();
        enemyAI.chaseCheck(alerted);
        enemyAttack.attackCheck();

        flipCheck();

        updateAnimationState();
    }

    private void updateAnimationState()
    {
        if (alerted)
        {
            anim.SetTrigger("Alerted");
        } 
    }

    private void flipCheck()
    {
        if (!alerted)
        {
            return;
        }

        if (isFacingRight && rPos < 0f || !isFacingRight && rPos > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
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
