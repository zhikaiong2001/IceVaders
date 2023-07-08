using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private EnemyMovement enemyMovement;
    private Boss1Movement boss1;
    private EnemyAI enemyAI;
    public bool isDead {  get; private set; }

    [Header("Health")]
    public int maxHealth;
    public int currentHealth;

    [Header("iFrames")]
    [SerializeField] private float hurtFramesDuration;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] private Shader defaultShader;
    [SerializeField] private Shader GUIShader;
    private SpriteRenderer spriteRend;

    [Header("Sounds")]
    [SerializeField] private AudioSource takeDamageSoundEffect;
    [SerializeField] private AudioSource dieSoundEffect;

    private void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAI = GetComponent<EnemyAI>();
        currentHealth = maxHealth;
        isDead = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
            StopAllCoroutines();
            StartCoroutine(Invulnerability());
            //takeDamageSoundEffect.Play();
        }
        else
        {
            if (!isDead)
            {
                anim.SetBool("isDead", true);
                anim.SetTrigger("Hurt");
                anim.SetTrigger("Die");
                rb.gravityScale = 1.0f;
                this.gameObject.layer = LayerMask.NameToLayer("Dead");
                foreach (Transform child in transform)
                {
                    child.gameObject.layer = LayerMask.NameToLayer("Dead");
                }
                isDead = true;
                //dieSoundEffect.Play();
                if (enemyAI != null)
                {
                    if (enemyAI.enabled)
                    {
                        enemyAI.enabled = false;
                        rb.gravityScale = 1f;
                    }
                }
            }

        }
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Attack"), LayerMask.NameToLayer("Enemy"), true);

        for (int i = 0; i < numberOfFlashes; i++)
        {
            yield return new WaitForSeconds(hurtFramesDuration / (2 * numberOfFlashes));
            spriteRend.material.shader = GUIShader;
            yield return new WaitForSeconds(hurtFramesDuration / (2 * numberOfFlashes));
            spriteRend.material.shader = defaultShader;
        }

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Attack"), LayerMask.NameToLayer("Enemy"), false);
    }

    private IEnumerator customEvent()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("End Screen");
    }
}
