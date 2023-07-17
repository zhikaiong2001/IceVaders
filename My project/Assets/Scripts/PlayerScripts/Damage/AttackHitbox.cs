using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private GameObject enemy;
    private EnemyKnockback ek;
    private EnemyHealth enemyHealth;
    public GameObject player;
    private Mana mana;
    private PlayerAttack playerAttack;

    private bool rightSide;

    private void Start()
    {
        mana = player.GetComponent<Mana>();
        playerAttack = player.GetComponent<PlayerAttack>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Breakable"))
        {
            collision.GetComponent<Breakable>().breakObject();
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
            if (Player.paybackActive)
            {
                enemyHealth.TakeDamage(Player.attackDamage * 2);
                Player.paybackActive = false;
            }
            else
            {
                enemyHealth.TakeDamage(Player.attackDamage);
            }

            mana.gainMana(playerAttack.manaGainPerAttack);
        }
    }
}
