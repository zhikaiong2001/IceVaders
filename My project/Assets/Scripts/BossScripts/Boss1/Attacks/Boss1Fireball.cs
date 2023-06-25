using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Fireball : MonoBehaviour
{
    // Componenets
    private Boss1Movement boss1Movement;
    private Animator animator;
    private Rigidbody2D rb;

    [Header("Waypoints")]
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypoint;
    private Vector3 curWaypointPos;

    [Header("Attributes")]
    public LayerMask damageLayers;
    public Transform firePosition;
    public GameObject fireball;
    public int damage;
    public float speed;
    public float fireTime;
    public float fireCooldown;
    private bool isFiring;
    public float startUpTime;
    public float endTime;

    [Header("Sounds")]
    public AudioSource fireballSoundEffect;

    void Start()
    {
        boss1Movement = GetComponent<Boss1Movement>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentWaypoint = 0;
        isFiring = false;
    }

    public void fireballCheck()
    {
        curWaypointPos = waypoints[currentWaypoint].transform.position;
        if (!isFiring)
        {
            transform.position = curWaypointPos;
            rb.velocity = Vector2.zero;
            Debug.Log("check");
            fire();
        }
    }

    IEnumerator fire()
    {
        isFiring = true;
        boss1Movement.disableMovement();
        animator.SetTrigger("FireballStart");
        yield return new WaitForSeconds(startUpTime);
        Instantiate(fireball, firePosition.position, transform.rotation);
        //fireballSoundEffect.Play();
        yield return new WaitForSeconds(endTime);
        animator.SetTrigger("FireballEnd");
        boss1Movement.enableMovement();
        isFiring = false;
        currentWaypoint++;

        if (currentWaypoint >= waypoints.Length)
        {
            currentWaypoint = 0;
            boss1Movement.nextAttack();
        }
    }
}
