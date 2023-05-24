using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] private float damage;
    [SerializeField] private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        this.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerMovement.setKBCounter(playerMovement.getKBTotalTime());
            playerMovement.setStunCounter(playerMovement.getStunTotal());
            
            if (collision.transform.position.x <= this.transform.position.x)
            {
                playerMovement.setKBRight(true);
            }
            else
            {
                playerMovement.setKBRight(false);
            }

            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
