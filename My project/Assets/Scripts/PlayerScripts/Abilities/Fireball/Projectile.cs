using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float projectileSpeed;
    public GameObject impactEffect;
    private bool isFacingRight;
    private int fireBallDamage;
    private GameObject enemy;

    private Rigidbody2D rb;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        isFacingRight = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().isFacingRight;
        fireBallDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<FireballSkill>().damage;
        projectileSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<FireballSkill>().speed;
        if (!isFacingRight)
        {
            transform.Rotate(0, 180f, 0);
            
        }
        rb.velocity = transform.right * projectileSpeed;
    }

    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemy = collision.gameObject.GetComponent<EnemyAttackHitbox>().enemy;
            enemy.GetComponent<EnemyHealth>().TakeDamage(fireBallDamage);
            Destroy(gameObject);
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }
    }
}
