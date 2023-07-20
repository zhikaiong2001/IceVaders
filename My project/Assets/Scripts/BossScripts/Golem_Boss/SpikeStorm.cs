using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeStorm : MonoBehaviour
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
        fireBallDamage = GameObject.FindGameObjectWithTag("Golem").GetComponent<GolemRanged1>().damage;
        //projectileSpeed = GameObject.FindGameObjectWithTag("Golem").GetComponent<GolemRanged1>().speed;
        rb.velocity = transform.right * projectileSpeed / Player.getDiffMult();
        Debug.Log(rb.velocity.x);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player = collision.GetComponent<PlayerCollisions>().player;
            kb = player.GetComponent<Knockback>();
            playerHealth = player.GetComponent<Health>();
            kb.knockCheck(rightSide);
            playerHealth.TakeDamage(fireBallDamage);
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }

   
}
