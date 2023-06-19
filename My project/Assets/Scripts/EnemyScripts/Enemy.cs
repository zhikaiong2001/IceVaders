using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    [Header ("Attributes")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private float damage;
    [SerializeField] private PlayerMovement2 playerMovement;
    [SerializeField] private bool isFlyingEnemy = false;

    [Header ("Hurt Frames")]
    [SerializeField] private float hurtFramesDuration;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] private Shader defaultShader;
    [SerializeField] private Shader GUIShader;
    private SpriteRenderer spriteRend;

    [Header ("Enemy Attack")]
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;
    public int attackDamage = 20;
    public float attackCoolDown = 1f;
    public float attackCounter;
    private Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        spriteRend = GetComponent<SpriteRenderer>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    void Update()
    {
        if (attackCounter > 0f)
        {
            attackCounter -= Time.deltaTime;
        }
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
        GetComponent<EnemyAI>().enabled = false;
        this.enabled = false;
        if (isFlyingEnemy)
        {
            Destroy(gameObject, 1.5f);
        }
        
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

    public void Attack()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<Health>().TakeDamage(attackDamage);
       
        }
      
    }

   
}
