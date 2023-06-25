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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(fireBallDamage);
            impact();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            impact();
        }
    }

    private void impact()
    {
        Destroy(gameObject);
        if (transform.right.x < 0f)
        {
            Vector2 explosionsPosition = new Vector2(transform.position.x -
                this.GetComponent<CircleCollider2D>().offset.x - this.GetComponent<CircleCollider2D>().radius * 2 - offSet, transform.position.y);
            Instantiate(impactEffect, explosionsPosition, transform.rotation);
        }
        else
        {
            Vector2 explosionsPosition = new Vector2(transform.position.x +
                this.GetComponent<CircleCollider2D>().offset.x + this.GetComponent<CircleCollider2D>().radius * 2 + offSet, transform.position.y);
            Instantiate(impactEffect, explosionsPosition, transform.rotation);
        }
    }
}
