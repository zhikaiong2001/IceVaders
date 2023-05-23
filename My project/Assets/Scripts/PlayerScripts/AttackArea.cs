using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 3;

    private void OnTriggerEnter2D(Collider2D collidder)
    {
        if (GetComponent<Collider>().GetComponent<Health>() != null)
        {
         //   Health health = GetComponent<Collider>().GetComponent<Health>();
            //health.Damage(damage);
        }
    }
}
