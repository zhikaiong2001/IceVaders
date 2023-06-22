using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private GameObject enemy;
    private EnemyKnockback ek;
    private EnemyHealth enemyHealth;

    private bool rightSide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //playerSoul.GainSoul(soulsPerAttack);
        if (collision.gameObject.tag == "Slime")
        {
            WaypointFollower wf = collision.GetComponent<WaypointFollower>();

            wf.setKBCounter(wf.getKBTotalTime());

            if (collision.transform.position.x <= this.transform.position.x)
            {
                wf.setKBRight(true);
            }
            else
            {
                wf.setKBRight(false);
            }

            collision.GetComponent<EnemyHealth>().TakeDamage(Player.attackDamage);
        }
        else if (collision.gameObject.tag == "Door")
        {
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemy = collision.GetComponent<EnemyAttackHitbox>().enemy;
            ek = enemy.GetComponent<EnemyKnockback>();
            enemyHealth = enemy.GetComponent<EnemyHealth>();

            if (collision.transform.position.x <= enemy.transform.position.x)
            {
                rightSide = true;
            }
            else
            {
                rightSide = false;
            }

            ek.knockCheck(rightSide);
            enemyHealth.TakeDamage(Player.attackDamage);
        }
    }
}
