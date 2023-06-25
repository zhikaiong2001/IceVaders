using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSkill : MonoBehaviour
{
    private bool fireballUnlocked;
    private bool canFire;
    private bool isFiring;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    private Mana mana;
    private Health health;

    [Header("Attributes")]
    public Transform firePosition;
    public GameObject fireball;
    public float cost;
    public int damage;
    public float speed;
    public float fireTime;
    public float fireCooldown;

    [Header("Sounds")]
    public AudioSource fireballSoundEffect;

    void Start()
    {
        canFire = true;
        isFiring = false;
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        mana = GetComponent<Mana>();
        health = GetComponent<Health>();
    }

    public void fireballCheck()
    {
        fireballUnlocked = Player.unlocked[(int)Player.Abilities.fireball];

        if (Input.GetKeyUp(KeyCode.C) && canFire && fireballUnlocked && !health.isHealing)
        {
            StartCoroutine(fire());
        }
    }

    IEnumerator fire()
    {
        if (Player.currentMana >= cost)
        {
            canFire = false;
            isFiring = true;
            playerMovement.disableMovement();
            float originalGravity = rb.gravityScale;
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
            Instantiate(fireball, firePosition.position, transform.rotation);
            mana.useMana(cost);
            //fireballSoundEffect.Play();
            yield return new WaitForSeconds(fireTime);
            rb.gravityScale = originalGravity;
            isFiring = false;
            playerMovement.enableMovement();
            yield return new WaitForSeconds(fireCooldown);
            canFire = true;    
        }
    }
}
