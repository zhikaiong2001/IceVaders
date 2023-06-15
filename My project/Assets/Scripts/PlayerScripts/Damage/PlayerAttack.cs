using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public Animator animator;
    private Rigidbody2D rb;
    public Transform attackPointTop;
    public Transform attackPointBottom;
    private float rectWidth = 0;
    private float rectHeight = 0;
    private float centreX = 0, centreY = 0;
    private Rect hitBox;
    public int attackDamage = 40;
    public LayerMask enemyLayers;

    // Attack Rate
    public float attackCooldown = 0f;
    private float nextAttackTime = 0f;

    // Attack Delay
    public float attackDelay = 0.2f;

    // Attack Active Frames
    public float attackDuration;
    private float durationEnd = 0f;


    private WaypointFollower wf;
    private EnemyKnockback ek;

    [SerializeField] private Soul playerSoul;
    [SerializeField] private float soulsPerAttack;

    [SerializeField] PlayerMovement playerMovement;

    private bool isAttacking = false;





    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rectWidth = attackPointBottom.position.x - attackPointTop.position.x;
        rectHeight = attackPointTop.position.y - attackPointBottom.position.y;
        centreX = rectWidth / 2;
        centreY = rectHeight / 2;
        hitBox = new Rect(centreX, centreY, rectWidth, rectHeight);
    }

    // Update is called once per frame
    public void attackCheck()
    {
        if (Time.time >= nextAttackTime && !playerMovement.isStunned)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                isAttacking = true;
                playerMovement.disableMovement();
                rb.velocity = new Vector2(0f, rb.velocity.y);
                StartCoroutine(wait(attackDelay));
                animator.SetTrigger("Attack");
                Attack();
                nextAttackTime = Time.time + attackCooldown;
                durationEnd = Time.time + attackDuration;
            }
        } 
        else if (Time.time < durationEnd)
        {
            Attack();
        }
        else
        {
            isAttacking = false;
            playerMovement.enableMovement();
        }
    }

    private IEnumerator wait(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    private void Attack()
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

    // Gizmos
    void OnDrawGizmosSelected()
    {
        if (attackPointTop == null || attackPointBottom == null)
        {
            return;
        }

        Gizmos.color = new Color(1.0f, 0.5f, 0.0f);
        DrawRect(hitBox);
        DrawRect(hitBox);
    }

    void OnDrawGizmos()
    {
        // Green
        Gizmos.color = new Color(0.0f, 1.0f, 0.0f);
        DrawRect(hitBox);
    }

    void DrawRect(Rect rect)
    {
        Gizmos.DrawWireCube(new Vector3(rect.center.x, rect.center.y, 0.01f), new Vector3(rect.size.x, rect.size.y, 0.01f));
    }
}
