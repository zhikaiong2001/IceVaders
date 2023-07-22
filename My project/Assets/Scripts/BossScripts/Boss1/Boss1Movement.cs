using Mono.Cecil.Cil;
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

    // State Control
    public bool canMove { get; private set; }
    [HideInInspector] public float rPos;
    [HideInInspector] public bool isFacingRight;
    private enum AttackState
    {
        fireball,
        flamingfrenzy,
        firestorm
    }
    private AttackState currentAttackState;
    private int currentAttackIndex;
    private AttackState[] attackOrder =
    {
        AttackState.fireball,
        AttackState.flamingfrenzy,
       AttackState.fireball,
        AttackState.firestorm
    };

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
        fireStorm = GetComponent<FireStorm>();
        isFacingRight = true;
        currentAttackIndex = 0;
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
        rPos = transform.position.x - player.position.x;

        if (isFacingRight && rPos > 0f || !isFacingRight && rPos < 0f)
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

    public void nextAttack()
    {
        currentAttackIndex++;
    }

    public void updateRPos()
    {
        rPos = transform.position.x - player.position.x;
    }
}
