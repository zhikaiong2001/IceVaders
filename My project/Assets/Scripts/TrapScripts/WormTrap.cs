using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormTrap : MonoBehaviour
{
    [SerializeField] private float timeBeforeFirstActivation = 0f;
    [SerializeField] private float cycleCooldown = 3f;
    [SerializeField] private float trapTime = 0.7f;
    private GameObject player;
    private BoxCollider2D colli;
    private Animator anim;
    private float timeLeft;
    private bool isAttacking;
    public AudioSource trapSound;
    public float soundMinDistance;
    // Start is called before the first frame update
    void Start()
    {
        colli = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        timeLeft = timeBeforeFirstActivation;
        isAttacking = false;
        player = GameObject.FindGameObjectWithTag("Player");

        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0f)
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
        trapSound.Play();
        if (Vector3.Distance(transform.position, player.transform.position) < soundMinDistance)
        {
            trapSound.volume = 0.20f;
        } else
        {
            trapSound.volume = 0f;
        }
        timeLeft = cycleCooldown + trapTime;
        yield return new WaitForSeconds(trapTime);
        anim.SetTrigger("Idle");
        colli.enabled = false;
        

    }
}
