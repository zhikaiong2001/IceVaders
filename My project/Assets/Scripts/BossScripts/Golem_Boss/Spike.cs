using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float projectileSpeed;
    //public GameObject impactEffect;
    private bool isFacingRight;
    private int fireBallDamage;
    public float offSet;

    private Rigidbody2D rb;
    private GameObject player;
    private Knockback kb;
    private EnemyAttack enemyAttack;
    private Health playerHealth;
    private bool rightSide;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isFacingRight = GameObject.FindGameObjectWithTag("Golem").GetComponent<GolemMovement>().isFacingRight;
        fireBallDamage = GameObject.FindGameObjectWithTag("Golem").GetComponent<GolemRanged1>().damage;
        projectileSpeed = GameObject.FindGameObjectWithTag("Golem").GetComponent<GolemRanged1>().speed;
        if (!isFacingRight)
        {
            transform.Rotate(0, 180f, 0);
        }
        rb.velocity = transform.right * projectileSpeed / Player.getDiffMult();
        Destroy(gameObject, 10f);
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
            playerHealth.TakeDamage(fireBallDamage);
            //impact();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
        Destroy(gameObject, 10f);

    }

   
}
