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
        if (collision.gameObject.tag == "Door")
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
            mana.gainMana(playerAttack.manaGainPerAttack);
        }
    }
}
