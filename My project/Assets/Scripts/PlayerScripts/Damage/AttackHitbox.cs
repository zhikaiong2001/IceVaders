using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
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
        else if (collision.gameObject.tag == "Enemy")
        {
            EnemyKnockback ek = collision.GetComponent<EnemyKnockback>();

            ek.knockCheck();

            collision.GetComponent<EnemyHealth>().TakeDamage(Player.attackDamage);
        }
    }
}
