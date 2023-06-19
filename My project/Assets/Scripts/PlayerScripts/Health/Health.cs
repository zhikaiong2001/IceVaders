using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        Player.currentHealth = Mathf.Clamp(Player.currentHealth - damage, 0, Player.maxHealth);

        if (Player.currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!Player.isDead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<PlayerCollisions>().enabled = false;
                rb.velocity = Vector2.zero;
                GetComponent<Rigidbody2D>().isKinematic = true;
                Player.isDead = true;
                StartCoroutine(respawn()); // replace with load
            }

        }
    }

    IEnumerator respawn()
    {
        yield return new WaitForSeconds(3);
        Player.currentHealth = 5;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Heal(int amount)
    {
        Player.currentHealth = Mathf.Clamp(Player.currentHealth + amount, 0, Player.maxHealth);
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
}
