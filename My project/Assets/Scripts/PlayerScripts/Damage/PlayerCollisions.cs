using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    private Knockback kb;
    private Health health;
    private bool rightSide;

    private void Start()
    {
        kb = player.GetComponent<Knockback>();
        health = player.GetComponent<Health>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
