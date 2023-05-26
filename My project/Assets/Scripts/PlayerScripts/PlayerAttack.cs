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

    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    private WaypointFollower wf;

    [SerializeField] PlayerMovement playerMovement;

    
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime && !playerMovement.isStunned)
        {
            if (Input.GetKeyDown(KeyCode.N))
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

            enemy.GetComponent<Enemy>().TakeDamage(attackDamage); 
        }
        
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
