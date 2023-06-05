using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public LayerMask enemyLayers;
    public Soul souls;
    public float soulsPerAttack;
    public bool attackUnlocked;

    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    private WaypointFollower wf;
    private EnemyKnockback ek;

    [SerializeField] private Soul playerSoul;
    [SerializeField] private float soulsPerAttack;

    [SerializeField] PlayerMovement playerMovement;





    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime && attackUnlocked)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }



    }

    private void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies =  Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            souls.GainSoul(soulsPerAttack);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "AttackSkillOrb")
        {
            attackUnlocked = true;
            Destroy(collision.gameObject);
        }

    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
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

    private IEnumerator Attack()
    {
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(attackDelay);

        Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(attackPointTop.position, attackPointBottom.position, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            playerSoul.GainSoul(soulsPerAttack);
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
}
