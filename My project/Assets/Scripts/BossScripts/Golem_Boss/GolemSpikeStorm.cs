using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class GolemSpikeStorm : MonoBehaviour
{
    private GolemMovement golemMovement;
    private Animator animator;
    public EnemyAttackHitbox attackHitbox;
    private Rigidbody2D rb;
    public LayerMask damageLayers;

    [Header("Attack")]
    public int attackDamage;
    public float attackCooldown;
    public float attackDelay;
    public float attackDuration;
    private bool isAttacking;
    private bool canAttack;

    [Header("Attributes")]
    [SerializeField] private float speed;

    public GameObject spikes;
    public Transform spikesPosition;

    void Start()
    {
        golemMovement = GetComponent<GolemMovement>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void spikeStormCheck()
    {
        StartCoroutine(SpikeStorm());
    }


    private IEnumerator SpikeStorm()
    {
        isAttacking = true;
        canAttack = false;
        golemMovement.disableMovement();
        rb.velocity = new Vector2(0f, 0f);
        // yield return new WaitForSeconds(attackDelay);
        animator.SetTrigger("SpikeStormStart");
        yield return new WaitForSeconds(attackDelay);
        Instantiate(spikes, spikesPosition.position, spikesPosition.rotation);
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
        golemMovement.enableMovement();
        animator.SetTrigger("SpikeStormEnd");
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        //Debug.Log("1");
        golemMovement.nextAttack();
    }
}
