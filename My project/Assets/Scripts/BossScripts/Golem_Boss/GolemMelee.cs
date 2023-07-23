using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class GolemMelee : MonoBehaviour
{
    private GolemMovement golemMovement;
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

    [Header("Attributes")]
    [SerializeField] private float speed;

    [Header("Sounds")]
    [SerializeField] private AudioSource meleeSound;

    void Start()
    {
        golemMovement = GetComponent<GolemMovement>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        starting = true;
        canAttack = true;
    }

    public void meleeCheck()
    {
        if (starting)
        {
            transform.position = start.transform.position;
            starting = false;
        }

        if (Vector2.Distance(end.transform.position, transform.position) < .01f)
        {
            golemMovement.nextAttack();
            starting = true;
        }
        else
        {
            if (canAttack && rangeCheck())
            {
                StopAllCoroutines();
                StartCoroutine(Melee());
            }
            if (!isAttacking)
            {
                transform.position = Vector2.MoveTowards(transform.position, end.transform.position, Time.deltaTime * speed);
            }
        }
    }

    private bool rangeCheck()
    {
        golemMovement.updateRPos();

        return Mathf.Abs(golemMovement.rPos) < range;
    }

    private IEnumerator Melee()
    {
        isAttacking = true;
        canAttack = false;
        golemMovement.disableMovement();
        rb.velocity = new Vector2(0f, 0f);
       // yield return new WaitForSeconds(attackDelay);
        animator.SetTrigger("MeleeStart");
        yield return new WaitForSeconds(attackDelay);
        animator.SetTrigger("MeleeTransition");
        yield return new WaitForSeconds(0.75f);
        attackHitbox.gameObject.SetActive(true);
        if (!GetComponent<EnemyHealth>().isDead)
        {
            meleeSound.Play();
        }
        
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
        animator.SetTrigger("MeleeEnd");
        golemMovement.enableMovement();
        attackHitbox.gameObject.SetActive(false);
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        golemMovement.nextAttack();
    }
}
