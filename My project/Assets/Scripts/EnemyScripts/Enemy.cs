using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    [Header ("Attributes")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    public int damage { get; private set; }
    [SerializeField] private PlayerMovement2 playerMovement;

    [Header ("Hurt Frames")]
    [SerializeField] private float hurtFramesDuration;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] private Shader defaultShader;
    [SerializeField] private Shader GUIShader;
    private SpriteRenderer spriteRend;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Hurt());
        }
    }

    private void Die()
    {
        animator.SetBool("IsDead", true);
        foreach (Collider2D c in GetComponents<Collider2D>()) 
        {
            c.enabled = false;
        }
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        this.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
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

    public bool deathStatus()
    {
        return currentHealth <= 0;
    }

    private IEnumerator Hurt()
    {
        for (int i = 0; i < numberOfFlashes; i++)
        {
            yield return new WaitForSeconds(hurtFramesDuration / (2 * numberOfFlashes));
            spriteRend.material.shader = GUIShader;
            yield return new WaitForSeconds(hurtFramesDuration / (2 * numberOfFlashes));
            spriteRend.material.shader = defaultShader;
        }
    }
}
