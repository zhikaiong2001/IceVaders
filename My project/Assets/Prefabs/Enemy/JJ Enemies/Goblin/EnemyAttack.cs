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

    // Attack Damage
    public int attackDamage;

    // Attack Rate
    public float attackCooldown;

    // Attack Delay
    public float attackDelay;

    // Attack Range
    public float range;

    // Attack Active Frames
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
        return Mathf.Abs(enemyMovement.rPos) < range;
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        canAttack = false;
        enemyMovement.disableMovement();
        rb.velocity = new Vector2(0f, 0f);
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
