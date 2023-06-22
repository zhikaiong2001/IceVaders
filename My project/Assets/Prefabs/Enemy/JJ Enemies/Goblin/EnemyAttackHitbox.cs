using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour
{
    private GameObject player;
    public GameObject enemy;
    private Knockback kb;
    private EnemyAttack enemyAttack;
    private Health playerHealth;

    private bool rightSide;

    private void Start()
    {
        enemyAttack = enemy.GetComponent<EnemyAttack>();
    }
     
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player = collision.GetComponent<PlayerCollisions>().player;
            kb = player.GetComponent<Knockback>();
            playerHealth = player.GetComponent<Health>();

            if (collision.transform.position.x <= enemy.transform.position.x)
            {
                rightSide = true;
            }
            else
            {
                rightSide = false;
            }

            kb.knockCheck(rightSide);
            playerHealth.TakeDamage(enemyAttack.attackDamage);
        }
    }
}
