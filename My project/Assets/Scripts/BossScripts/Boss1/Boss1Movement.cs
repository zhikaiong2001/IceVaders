using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Movement : MonoBehaviour
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
    public float rPos { get; private set; }
    public float alertDist;
    [HideInInspector] public bool isFacingRight;
    private enum AttackState { fireball, flamingfrenzy, firestorm }
    private AttackState currentAttackState;
    private int currentAttackIndex;
    private AttackState[] attackOrder = { AttackState.fireball, AttackState.flamingfrenzy, AttackState.fireball, AttackState.firestorm };

    // Animation State
    private string currentState;
    private enum MovementState { idle, alerted };

    // Add-Ons
    private EnemyHealth enemyHealth;
    private FlamingFrenzy flamingFrenzy;
    private Boss1Fireball boss1Fireball;
    private FireStorm fireStorm;

    private void Start()
    {
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        flamingFrenzy = GetComponent<FlamingFrenzy>();
        boss1Fireball = GetComponent<Boss1Fireball>();
        isFacingRight = true;
        currentAttackIndex = -1;
    }


    private void Update()
    {
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

        if (currentAttackIndex >= attackOrder.Length)
        {
            currentAttackIndex = 0;
        }
        currentAttackState = attackOrder[currentAttackIndex];

        rPos = transform.position.x - player.position.x;

        flipCheck();

        if (currentAttackState == AttackState.flamingfrenzy)
        {
            flamingFrenzy.frenzyCheck();
        }
        else if (currentAttackState == AttackState.fireball)
        {
            boss1Fireball.fireballCheck();
        }
        else if (currentAttackState == AttackState.firestorm)
        {
            fireStorm.fireStormCheck();
        }
    }

    public void flipCheck()
    {
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

    public void nextAttack()
    {
        currentAttackIndex++;
    }
}
