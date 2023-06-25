using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1AttackHitbox : MonoBehaviour
{
    private GameObject player;
    public GameObject boss;
    private Knockback kb;
    private Health playerHealth;

    private bool rightSide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player = collision.GetComponent<PlayerCollisions>().player;
            kb = player.GetComponent<Knockback>();
            playerHealth = player.GetComponent<Health>();

            if (collision.transform.position.x <= boss.transform.position.x)
            {
                rightSide = true;
            }
            else
            {
                rightSide = false;
            }

            kb.knockCheck(rightSide);
            playerHealth.TakeDamage(1);
        }
    }
}
