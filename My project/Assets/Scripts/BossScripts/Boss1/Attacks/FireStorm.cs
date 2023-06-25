using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;

public class FireStorm : MonoBehaviour
{
    // Componenets
    private Boss1Movement boss1Movement;
    private Animator animator;
    private Rigidbody2D rb;

    [Header("Waypoints")]
    [SerializeField] private GameObject[] firePointObjects;
    private Vector2[] firePoints;
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
    private Vector2 currentFirePoint;

    [Header("Sounds")]
    public AudioSource fireballSoundEffect;

    void OnAwake()
    {
        boss1Movement = GetComponent<Boss1Movement>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        firePoints[0] = firePointObjects[0].transform.position;
        firePoints[1] = firePointObjects[1].transform.position;
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
            currentFirePoint = new Vector2(firePoints[0].x + (float) rnd.NextDouble() * (firePoints[0].x - firePoints[1].x), firePoints[0].y);
            fire(currentFirePoint);
            yield return new WaitForSeconds(fireDelay);
        }
        animator.SetTrigger("FireStormEnd");
        yield return new WaitForSeconds(endTime);
        boss1Movement.enableMovement();
        isFiring = false;
        boss1Movement.nextAttack();
    }

    private void fire(Vector2 firePosition)
    {
        Instantiate(fireball, firePosition, quaternion.identity);
        //fireballSoundEffect.Play();
    }
}
