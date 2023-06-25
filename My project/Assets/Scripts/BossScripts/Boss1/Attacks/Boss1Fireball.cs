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
    private bool isFiring;
    public float startUpTime;
    public float fireTime;
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
            //Debug.Log("check");
            StartCoroutine(fire());
        }
    }

    IEnumerator fire()
    {
        isFiring = true;
        boss1Movement.disableMovement();
        boss1Movement.flipCheck();
        Debug.Log(boss1Movement.isFacingRight);
        yield return new WaitForSeconds(startUpTime);
        animator.SetTrigger("FireballStart");
        yield return new WaitForSeconds(fireTime);
        Instantiate(fireball, firePosition.position, transform.rotation);
        //fireballSoundEffect.Play();
        animator.SetTrigger("FireballEnd");
        yield return new WaitForSeconds(endTime);
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
