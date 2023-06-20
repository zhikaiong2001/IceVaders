using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.ShaderData;

public class EnemyHealth : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private EnemyMovement enemyMovement;
    public bool isDead {  get; private set; }

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
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void TakeDamage(int damage)
    {
        Player.currentHealth = Mathf.Clamp(Player.currentHealth - damage, 0, Player.maxHealth);

        if (Player.currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
            StartCoroutine(Invulnerability());
            //takeDamageSoundEffect.Play();
        }
        else
        {
            if (!Player.isDead)
            {
                anim.SetBool("isDead", true);
                enemyMovement.enabled = false;
                rb.velocity = Vector2.zero;
                this.gameObject.layer = LayerMask.NameToLayer("Dead");
                isDead = true;
                //dieSoundEffect.Play();
            }

        }
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyHurtLayer"), true);

        for (int i = 0; i < numberOfFlashes; i++)
        {
            yield return new WaitForSeconds(hurtFramesDuration / (2 * numberOfFlashes));
            spriteRend.material.shader = GUIShader;
            yield return new WaitForSeconds(hurtFramesDuration / (2 * numberOfFlashes));
            spriteRend.material.shader = defaultShader;
        }

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyHurtLayer"), false);
    }
}
