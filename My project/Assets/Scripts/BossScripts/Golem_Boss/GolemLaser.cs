using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemLaser : MonoBehaviour
{
    // Componenets
    private GolemMovement golemMovement;
    private Animator animator;
    private Rigidbody2D rb;

    [Header("Attributes")]
    public LayerMask damageLayers;
    public int damage;
    public float speed;
    private bool isFiring;
    public float startUpTime;
    public float fireTime;
    public float endTime;

    [Header("Laser")]
    public GameObject laserStart;
    public GameObject laserFire;

    [Header("Sounds")]
    public AudioSource fireballSoundEffect;

    void Start()
    {
        golemMovement = GetComponent<GolemMovement>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isFiring = false;
    }

    public void golemLaserCheck()
    {
        //Debug.Log("1");
        if (!isFiring)
        {
            rb.velocity = Vector2.zero;
            Debug.Log("laser");
            StartCoroutine(fire());
            
        }
    }

    IEnumerator fire()
    {
        isFiring = true;
        golemMovement.disableMovement();
        golemMovement.flipCheck();
        //Debug.Log(golemMovement.isFacingRight);
        laserStart.SetActive(true);
        yield return new WaitForSeconds(startUpTime);
        laserFire.SetActive(true);
        laserStart.SetActive(false);
        yield return new WaitForSeconds(fireTime);
        laserFire.SetActive(false);
        yield return new WaitForSeconds(endTime);
        golemMovement.enableMovement();
        isFiring = false;
        golemMovement.nextAttack();
    }
}
