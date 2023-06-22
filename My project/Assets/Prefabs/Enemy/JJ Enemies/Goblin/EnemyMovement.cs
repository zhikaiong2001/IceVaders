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

    [Header("Chase Attributes")]
    public float speed;


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
    private EnemyHealth enemyHealth;

    private void Start()
    {
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        enemyIdling = GetComponent<EnemyIdling>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyHealth = GetComponent<EnemyHealth>();
        alerted = false;
        isFacingRight = true;
    }


    private void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            Debug.Log(isFacingRight);
        }

        if (enemyHealth.isDead)
        {
            return;
        }

        if (!canMove)
        {
            return;
        }

        if (PauseMenu.GameIsPaused)
        {
            return;
        }

        rPos = transform.position.x - player.position.x;

        if (Mathf.Abs(rPos) < alertDist)
        {
            alerted = true;
        }

        enemyIdling.idleCheck();

        if (alerted)
        {
            Vector2 newPos = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, newPos, Time.deltaTime * speed);
        }

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

        if (isFacingRight && rPos > 0f || !isFacingRight && rPos < 0f)
        {
            Debug.Log("check");
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
