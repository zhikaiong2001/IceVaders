using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemRanged1 : MonoBehaviour
{
    // Componenets
    private GolemMovement golemMovement;
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
        golemMovement = GetComponent<GolemMovement>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentWaypoint = 0;
        isFiring = false;
    }

    public void ranged1Check()
    {
        curWaypointPos = waypoints[currentWaypoint].transform.position;
        //Debug.Log("1");
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
        golemMovement.disableMovement();
        golemMovement.flipCheck();
        Debug.Log(golemMovement.isFacingRight);
        yield return new WaitForSeconds(startUpTime);
        animator.SetTrigger("Ranged1Start");
        yield return new WaitForSeconds(fireTime);
        if (GetComponent<EnemyHealth>().isDead)
        {
            yield break;
        }
        
        Instantiate(fireball, firePosition.position, transform.rotation);
        fireballSoundEffect.Play();
        animator.SetTrigger("Ranged1End");
        yield return new WaitForSeconds(endTime);
        golemMovement.enableMovement();
        isFiring = false;
        currentWaypoint++;

        if (currentWaypoint >= waypoints.Length)
        {
            currentWaypoint = 0;
            golemMovement.nextAttack();
        }
    }
}
