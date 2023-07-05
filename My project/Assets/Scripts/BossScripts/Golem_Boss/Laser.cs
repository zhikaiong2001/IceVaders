using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    private Rigidbody2D rb;
    private GameObject player;
    private Knockback kb;
    private EnemyAttack enemyAttack;
    private Health playerHealth;
    private bool rightSide;
    private int laserDamage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        laserDamage = GameObject.FindGameObjectWithTag("Golem").GetComponent<GolemLaser>().damage;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player = collision.GetComponent<PlayerCollisions>().player;
            kb = player.GetComponent<Knockback>();
            playerHealth = player.GetComponent<Health>();

            if (collision.transform.position.x <= this.transform.position.x)
            {
                rightSide = true;
            }
            else
            {
                rightSide = false;
            }

            kb.knockCheck(rightSide);
            playerHealth.TakeDamage(laserDamage);
            //impact();
        }

        

    }


}
