using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public AttackHitbox attackHitbox;
    public Animator animator;
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    public LayerMask enemyLayers;

    // Attack Rate
    public float attackCooldown;

    // Attack Delay
    public float attackDelay;

    // Attack Active Frames
    public float attackDuration;

    private WaypointFollower wf;
    private EnemyKnockback ek;

    [SerializeField] private Soul playerSoul;
    [SerializeField] private float soulsPerAttack;

    private bool isAttacking = false;
    private bool canAttack = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>(); 
    }

    // Update is called once per frame
    public void attackCheck()
    {
        if (canAttack && !isAttacking && Input.GetKeyDown(KeyCode.X) && Player.unlocked[(int)Player.Abilities.sword])
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        canAttack = false;
        playerMovement.disableMovement();
        rb.velocity = new Vector2(rb.velocity.x * 0.5f, 0f);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackDelay);
        attackHitbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
        attackHitbox.gameObject.SetActive(false);
        playerMovement.enableMovement();
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
