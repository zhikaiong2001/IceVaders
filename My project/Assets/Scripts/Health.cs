using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth;
    private Animator anim;
    private Rigidbody2D rb;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [SerializeField] private AudioSource takeDamageSoundEffect;
    [SerializeField] private AudioSource dieSoundEffect;

    private void Awake()
    {
        currentHealth = PlayerStatic.health;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        PlayerStatic.health = currentHealth;

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
            takeDamageSoundEffect.Play();
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement2>().enabled = false;
                rb.velocity = Vector3.zero;
                dead = true;
                StartCoroutine(deathProcess());
                dieSoundEffect.Play();
            }
           
        }
    }

    IEnumerator deathProcess()
    {
        yield return new WaitForSeconds(3);
        PlayerStatic.health = 5;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, startingHealth);
    }


    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes));
        }
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);

    }


    // Getters and Setters for Saves
    public float getMaxHealth()
    {
        return this.startingHealth;
    }

    public void setMaxHealth(float health)
    {
        startingHealth = health;
    }
}
