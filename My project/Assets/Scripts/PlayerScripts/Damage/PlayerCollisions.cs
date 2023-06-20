using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public Knockback kb;
    private Health health;

    private void Start()
    {
        health = kb.GetComponent<Health>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyHurtLayer"))
        {
            if (this.transform.position.x <= collision.transform.position.x)
            {
                kb.fromRight = true;
            }
            else
            {
                kb.fromRight = false;
            }

            health.TakeDamage(collision.GetComponent<Enemy>().damage);
            kb.knockCheck();
        }
    }
}
