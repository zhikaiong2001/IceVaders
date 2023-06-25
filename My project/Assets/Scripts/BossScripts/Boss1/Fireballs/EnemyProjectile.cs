using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float projectileSpeed;
    public GameObject impactEffect;
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
        isFacingRight = GameObject.FindGameObjectWithTag("FireApostle").GetComponent<Boss1Movement>().isFacingRight;
        fireBallDamage = GameObject.FindGameObjectWithTag("FireApostle").GetComponent<Boss1Fireball>().damage;
        projectileSpeed = GameObject.FindGameObjectWithTag("FireApostle").GetComponent<Boss1Fireball>().speed;
        if (!isFacingRight)
        {
            transform.Rotate(0, 180f, 0);
        }
        rb.velocity = transform.right * projectileSpeed;
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
            //impact();
        }
    }

    private void impact()
    {
        Destroy(gameObject);
        if (transform.right.x < 0f)
        {
            Vector2 explosionsPosition = new Vector2(-1 * (transform.position.x -
                this.GetComponent<CircleCollider2D>().offset.x - this.GetComponent<CircleCollider2D>().radius * 2 - offSet), transform.position.y);
            Instantiate(impactEffect, explosionsPosition, transform.rotation);
        }
        else
        {
            Vector2 explosionsPosition = new Vector2(-1 * (transform.position.x +
                this.GetComponent<CircleCollider2D>().offset.x + this.GetComponent<CircleCollider2D>().radius * 2 + offSet), transform.position.y);
            Instantiate(impactEffect, explosionsPosition, transform.rotation);
        }
    }
}
