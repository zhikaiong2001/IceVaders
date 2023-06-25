using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class FlamingFrenzy : MonoBehaviour
{
    private Boss1Movement boss1Movement;
    private Animator animator;
    public EnemyAttackHitbox enemyAttackHitbox;
    private Rigidbody2D rb;
    public LayerMask damageLayers;

    [Header("Waypoints")]
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject end;
    private bool starting;

    [Header("Attack")]
    public int attackDamage;
    public float attackCooldown;
    public float attackDelay;
    public float range;
    public float attackDuration;
    private bool isAttacking;
    private bool canAttack;

    [Header("Attributes")]
    [SerializeField] private float speed;

    void Start()
    {
        boss1Movement = GetComponent<Boss1Movement>();
        animator = GetComponent<Animator>();
        starting = true;
    }

    public void frenzyCheck()
    {
        if (starting)
        {
            transform.position = start.transform.position;
            starting = false;
        }

        if (Vector2.Distance(end.transform.position, transform.position) < .01f)
        {
            boss1Movement.nextAttack();
            starting = true;
        }
        else
        {
            if (rangeCheck())
            {
                StartCoroutine(Attack());
            }
            if (!isAttacking)
            {
                transform.position = Vector2.MoveTowards(transform.position, end.transform.position, Time.deltaTime * speed);
            }
        }
    }

    private bool rangeCheck()
    {
        return Mathf.Abs(boss1Movement.rPos) < range;
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        canAttack = false;
        boss1Movement.disableMovement();
        rb.velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(attackDelay);
        animator.SetTrigger("FlamingFrenzyStart");
        enemyAttackHitbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
        boss1Movement.enableMovement();
        enemyAttackHitbox.gameObject.SetActive(false);
        animator.SetTrigger("FlamingFrenzyEnd");
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
