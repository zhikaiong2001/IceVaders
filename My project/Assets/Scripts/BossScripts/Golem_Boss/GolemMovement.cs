using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemMovement : MonoBehaviour
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
        golemMelee,
        golemRanged1,
        golemSpikeStorm,
        golemLaser
         
        
    }
    private AttackState currentAttackState;
    private int currentAttackIndex;
    private AttackState[] attackOrder =
    {
        AttackState.golemMelee,
        AttackState.golemRanged1,
        AttackState.golemMelee,
        AttackState.golemSpikeStorm,
        AttackState.golemMelee,
        AttackState.golemLaser
        
    };

    // Add-Ons
    private EnemyHealth enemyHealth;
    private GolemMelee golemMelee;
    private GolemRanged1 golemRanged1;
    private GolemSpikeStorm golemSpikeStorm;
    private GolemLaser golemLaser;

    private void Start()
    {
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        golemMelee = GetComponent<GolemMelee>();
        golemRanged1 = GetComponent<GolemRanged1>();
        golemSpikeStorm = GetComponent<GolemSpikeStorm>();
        golemLaser = GetComponent<GolemLaser>();
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

        if (currentAttackState == AttackState.golemMelee)
        {
            golemMelee.meleeCheck();
        }
        else if (currentAttackState == AttackState.golemRanged1)
        {
            golemRanged1.ranged1Check();
        }
        else if (currentAttackState == AttackState.golemSpikeStorm)
        {
            golemSpikeStorm.spikeStormCheck();
        } else if (currentAttackState == AttackState.golemLaser)
        {
            golemLaser.golemLaserCheck();
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
