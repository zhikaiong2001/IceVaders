using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    [Header ("Attributes")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    public int damage;
    [SerializeField] private PlayerMovement playerMovement;

    [Header ("Hurt Frames")]
    [SerializeField] private float hurtFramesDuration;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] private Shader defaultShader;
    [SerializeField] private Shader GUIShader;
    private SpriteRenderer spriteRend;

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
