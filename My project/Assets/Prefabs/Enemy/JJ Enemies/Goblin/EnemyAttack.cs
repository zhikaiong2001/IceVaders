using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyAttackHitbox enemyAttackHitbox;
    private Animator animator;
    private Rigidbody2D rb;
    private EnemyMovement enemyMovement;
    public LayerMask damageLayers;

    [Header("Attack")]
    public int attackDamage;
    public float attackCooldown;
    public float windupTransTime;
    public float windupTime;
    public float attackDelay;
    public float range;
    public float attackDuration;
    private bool isAttacking;
    private bool canAttack;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<EnemyMovement>();
        canAttack = true;
        isAttacking = false;
    }

    // Update is called once per frame
    public void attackCheck()
    {
        if (canAttack && rangeCheck())
        {
            StartCoroutine(Attack());
        }
    }

    private bool rangeCheck()
    {
        if (range < 0)
        {
            return false;
        }

        return Mathf.Abs(enemyMovement.rXPos) < range;
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        canAttack = false;
        enemyMovement.disableMovement();
        rb.velocity = new Vector2(0f, 0f);
        animator.SetTrigger("Windup");
        yield return new WaitForSeconds(windupTransTime);
        yield return new WaitForSeconds(windupTime * Player.getDiffMult());
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackDelay);
        enemyAttackHitbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
        enemyMovement.enableMovement();
        enemyAttackHitbox.gameObject.SetActive(false);
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
