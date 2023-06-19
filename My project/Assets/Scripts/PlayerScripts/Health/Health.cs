using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private BoxCollider2D bc;
    public PlayerCollisions playerCollisions;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;

    [SerializeField] private AudioSource takeDamageSoundEffect;
    [SerializeField] private AudioSource dieSoundEffect;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void TakeDamage(int damage)
    {
        Player.currentHealth = Mathf.Clamp(Player.currentHealth - damage, 0, Player.maxHealth);

        if (Player.currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
            takeDamageSoundEffect.Play();
        }
        else
        {
            if (!Player.isDead)
            {
                anim.SetTrigger("die");
<<<<<<< HEAD:My project/Assets/Scripts/PlayerScripts/Health/Health.cs
                playerMovement.enabled = false;
                playerCollisions.gameObject.SetActive(false);
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;
                bc.enabled = false;
                Player.isDead = true;
                StartCoroutine(respawn()); // replace with load
=======
                GetComponent<PlayerMovement2>().enabled = false;
                rb.velocity = Vector3.zero;
                dead = true;
                StartCoroutine(deathProcess());
                dieSoundEffect.Play();
>>>>>>> ba42d5f2fd5cac182014ef2746347cd660d49435:My project/Assets/Scripts/Health.cs
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
