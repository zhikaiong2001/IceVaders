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
    public PlayerCollisions playerCollisions;
    private Player player;
    private SpriteRenderer sprite;

    [Header("Healing")]
    public int healAmount;
    public float healCost;
    public float initiationTime;
    private float initiationCounter;
    public float healTime;
    private float healCounter;
    public bool isHealing { get; private set; }
    private Color originalColor;
    public Color healColor;
    private bool initialPress;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;

    [Header("Sounds")]
    [SerializeField] private AudioSource takeDamageSoundEffect;
    [SerializeField] private AudioSource dieSoundEffect;
    [SerializeField] private AudioSource healSoundEffect;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        isHealing = false;
        initiationCounter = initiationTime;
        healCounter = healTime;
        player = GetComponent<Player>();
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
        initialPress = true;
    }

    public void TakeDamage(int damage)
    {
        Player.currentHealth = Mathf.Clamp(Player.currentHealth - damage, 0, Player.maxHealth);

        if (Player.currentHealth > 0)
        {
            if (Player.unlockCheck((int)Player.Abilities.payback))
            {
                Player.paybackActive = true;
            }
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
            //takeDamageSoundEffect.Play();
        }
        else
        {
            if (!Player.isDead)
            {
                anim.SetTrigger("die");
                playerMovement.enabled = false;
                playerCollisions.gameObject.SetActive(false);
                rb.velocity = Vector2.zero;
                this.gameObject.layer = LayerMask.NameToLayer("Dead");
                Player.isDead = true;
                StartCoroutine(respawn());
                //dieSoundEffect.Play();
            }

        }
    }

    IEnumerator respawn()
    {
        yield return new WaitForSeconds(3);
        player.LoadPlayer();
    }

    public void healCheck()
    {
        if (initialPress)
        {
            rb.velocity = Vector2.zero;
            initialPress = false;
        }

        if (Input.GetKey(KeyCode.C) && Player.currentMana >= healCost)
        {
            if (initiationCounter > 0f)
            {
                initiationCounter -= Time.deltaTime;
            }
            else
            {
                if (healCounter > 0f)
                {
                    healCounter -= Time.deltaTime;
                    isHealing = true;
                    sprite.color = healColor;
                    playerMovement.disableMovement();
                }
                else
                {
                    heal();
                    sprite.color = originalColor;
                    playerMovement.enableMovement();
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            healCounter = healTime;
            isHealing = false;
            initiationCounter = initiationTime;
            sprite.color = originalColor;
            playerMovement.enableMovement();
            initialPress = true;
        }
    }

    public void heal()
    {
        Player.currentHealth = Mathf.Clamp(Player.currentHealth + healAmount, 0, Player.maxHealth);
        Player.currentMana = Mathf.Clamp(Player.currentMana - healCost, 0, Player.maxMana);
        //healSoundEffect.Play();
        healCounter = healTime;
        isHealing = false;
    }


    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyAttack"), true);

        yield return new WaitForSeconds(iFramesDuration);

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyAttack"), false);
    }
}
