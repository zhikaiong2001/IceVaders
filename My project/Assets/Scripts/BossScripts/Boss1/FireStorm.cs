using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FireStorm : MonoBehaviour
{
    // Componenets
    private Boss1Movement boss1Movement;
    private Animator animator;
    private Rigidbody2D rb;

    [Header("Waypoints")]
    [SerializeField] private GameObject[] firePoints;
    public GameObject channelPoint;

    [Header("Attributes")]
    public LayerMask damageLayers;
    public GameObject fireball;
    public int damage;
    public float speed;
    public float fireTime;
    public float fireCooldown;
    private bool isFiring;
    public float startUpTime;
    public float endTime;

    public int numberOfFires;
    public float fireDelay;
    private GameObject currentFirePoint;

    [Header("Sounds")]
    public AudioSource fireballSoundEffect;

    void Start()
    {
        boss1Movement = GetComponent<Boss1Movement>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void fireStormCheck()
    {
        transform.position = channelPoint.transform.position;
        rb.velocity = Vector2.zero;
        StartCoroutine(storm());
    }

    IEnumerator storm()
    {
        isFiring = true;
        boss1Movement.disableMovement();
        animator.SetTrigger("FireStormStart");
        yield return new WaitForSeconds(startUpTime);
        for (int i = 0; i < numberOfFires; i++)
        {
            System.Random rnd = new System.Random();
            currentFirePoint = firePoints[rnd.Next(6)];
            fire(currentFirePoint);
            yield return new WaitForSeconds(fireDelay);
        }
        animator.SetTrigger("FireStormEnd");
        yield return new WaitForSeconds(endTime);
        boss1Movement.enableMovement();
        isFiring = false;
        boss1Movement.nextAttack();
    }

    private void fire(GameObject firePosition)
    {
        Instantiate(fireball, firePosition.transform.position, firePosition.transform.rotation);
        //fireballSoundEffect.Play();
    }
}
