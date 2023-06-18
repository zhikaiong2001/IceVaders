using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private Knockback kb;

    private void Start()
    {
        kb = GetComponent<Knockback>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (this.transform.position.x <= collision.transform.position.x)
            {
                kb.fromRight = true;
            }
            else
            {
                kb.fromRight = false;
            }

            GetComponent<Health>().TakeDamage(collision.GetComponent<Enemy>().damage);
            kb.knockCheck();
        }
    }
}
