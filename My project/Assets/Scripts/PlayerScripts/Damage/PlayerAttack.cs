using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public AttackHitbox attackHitbox;
    public Animator animator;
    private Rigidbody2D rb;
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

    [SerializeField] PlayerMovement playerMovement;

    private bool isAttacking = false;
    private bool canAttack = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void attackCheck()
    {
        if (canAttack && Input.GetKeyDown(KeyCode.X) && Player.unlocked[(int)Player.Abilities.sword])
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        canAttack = false;
        playerMovement.disableMovement();
        rb.velocity = new Vector2(0f, rb.velocity.y);
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

    /*private void activateHitbox()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(attackPointTop.position, attackPointBottom.position, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            //playerSoul.GainSoul(soulsPerAttack);
            if (enemy.gameObject.tag == "Slime")
            {
                wf = enemy.GetComponent<WaypointFollower>();

                wf.setKBCounter(wf.getKBTotalTime());

                if (enemy.transform.position.x <= this.transform.position.x)
                {
                    wf.setKBRight(true);
                }
                else
                {
                    wf.setKBRight(false);
                }
            }
            else if (enemy.gameObject.tag == "Door")
            {
                enemy.gameObject.SetActive(false);
            }
            else
            {
                ek = enemy.GetComponent<EnemyKnockback>();

                ek.setKBCounter(ek.getKBTotalTime());

                if (enemy.transform.position.x <= this.transform.position.x)
                {
                    ek.setKBRight(true);
                }
                else
                {
                    ek.setKBRight(false);
                }
            }

            if (enemy.tag != "Door")
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }
    }

    private void deactivateHitbox()
    {
        return;
    }*/
}
