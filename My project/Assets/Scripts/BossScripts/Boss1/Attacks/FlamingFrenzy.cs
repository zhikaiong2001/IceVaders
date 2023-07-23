using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class FlamingFrenzy : MonoBehaviour
{
    private Boss1Movement boss1Movement;
    private Animator animator;
    public EnemyAttackHitbox attackHitbox;
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
    public float windupTransTime;
    public float windupTime;

    [Header("Attributes")]
    [SerializeField] private float speed;

    [SerializeField] private GameObject fireSound;

    void Start()
    {
        boss1Movement = GetComponent<Boss1Movement>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        starting = true;
        canAttack = true;
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
            if (canAttack && rangeCheck())
            {
                StopAllCoroutines();
                StartCoroutine(SpewFire());
            }
            if (!isAttacking)
            {
                transform.position = Vector2.MoveTowards(transform.position, end.transform.position, Time.deltaTime * speed);
            }
        }
    }

    private bool rangeCheck()
    {
        boss1Movement.updateRPos();

        return Mathf.Abs(boss1Movement.rPos) < range;
    }

    private IEnumerator SpewFire()
    {
        isAttacking = true;
        canAttack = false;
        boss1Movement.disableMovement();
        rb.velocity = new Vector2(0f, 0f);
        animator.SetTrigger("Windup");
        yield return new WaitForSeconds(windupTransTime);
        yield return new WaitForSeconds(windupTime * Player.getDiffMult());
        animator.SetTrigger("FlamingFrenzyStart");
        yield return new WaitForSeconds(attackDelay);
        if (GetComponent<EnemyHealth>().isDead)
        {
            yield break;
        }
        attackHitbox.gameObject.SetActive(true);
        fireSound.GetComponent<AudioSource>().enabled = true;
        yield return new WaitForSeconds(attackDuration);
        fireSound.GetComponent<AudioSource>().enabled = false;
        isAttacking = false;
        boss1Movement.enableMovement();
        attackHitbox.gameObject.SetActive(false);
        animator.SetTrigger("FlamingFrenzyEnd");
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
