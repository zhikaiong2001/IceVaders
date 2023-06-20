using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour
{
    private Knockback kb;
    public EnemyAttack enemyAttack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerHurtLayer"))
        {
            kb = collision.GetComponent<Knockback>();

            kb.knockCheck();

            if (collision.transform.position.x <= this.transform.position.x)
            {
                kb.fromRight = true;
            }
            else
            {
                kb.fromRight = false;
            }

            collision.GetComponent<Health>().TakeDamage(enemyAttack.attackDamage);
        }
    }
}
