using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    public GameObject impactEffect;
    private bool isFacingRight;
    private int fireBallDamage;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {


        rb = GetComponent<Rigidbody2D>();
        isFacingRight = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement2>().isFacingRight;
        fireBallDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSoulSkills>().fireBallDamage;
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
            Debug.Log("hit");
            //Destroy(collision.gameObject);
            collision.gameObject.GetComponent<Enemy>().TakeDamage(fireBallDamage);
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
