using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormTrap : MonoBehaviour
{
    [SerializeField] private float timeBeforeFirstActivation = 0f;
    [SerializeField] private float cycleCooldown = 3f;
    [SerializeField] private float trapDamage = 1f;
    [SerializeField] private float trapTime = 0.7f;
    private GameObject player;
    private BoxCollider2D colli;
    private Animator anim;
    private float timeLeft;
    private bool isAttacking;
    // Start is called before the first frame update
    void Start()
    {
        colli = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        timeLeft = timeBeforeFirstActivation;
        isAttacking = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0.1f)
        {
            timeLeft -= Time.deltaTime;
        } else
        {
            if(!isAttacking)
            {
                StartCoroutine(Attack());
                isAttacking = true;
            } else
            {
                isAttacking=false;
            }
            
        }
        
    }

    private IEnumerator Attack()
    {
        anim.SetTrigger("Attack");
        colli.enabled = true;
        yield return new WaitForSeconds(trapTime);
        anim.SetTrigger("Idle");
        colli.enabled = false;
        timeLeft = cycleCooldown;

    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<Health>().TakeDamage((int)trapDamage);
        }
    }
}
