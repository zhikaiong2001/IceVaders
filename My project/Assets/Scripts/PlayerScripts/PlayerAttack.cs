using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public Animator animator;

    public Transform attackPointTop;
    public Transform attackPointBottom;
    private float rectWidth = 0;
    private float rectHeight = 0;
    private float centreX = 0, centreY = 0;
    private Rect hitBox;
    public int attackDamage = 40;
    public LayerMask enemyLayers;

    public float attackRate = 2f;
    public float attackDelay = 0.2f;
    private float nextAttackTime = 0f;

    private WaypointFollower wf;
    private EnemyKnockback ek;

    [SerializeField] PlayerMovement playerMovement;

    
    

    // Start is called before the first frame update
    void Start()
    {
        rectWidth = attackPointBottom.position.x - attackPointTop.position.x;
        rectHeight = attackPointTop.position.y - attackPointBottom.position.y;
        centreX = rectWidth / 2;
        centreY = rectHeight / 2;
        hitBox = new Rect(centreX, centreY, rectWidth, rectHeight);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime && !playerMovement.isStunned)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                StartCoroutine(Attack());
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

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

    private IEnumerator Attack()
    {
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(attackDelay);

        Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(attackPointTop.position, attackPointBottom.position, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
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
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }
}
