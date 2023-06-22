using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class EnemyIdling : MonoBehaviour
{
    private EnemyMovement enemyMovement;
    private Animator animator;
    private float wRPos;

    [Header("Waypoint")]
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypoint = 0;

    [Header("Attributes")]
    [SerializeField] private float speed;
    public float waitTime;
    private float waitCounter;

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        animator = GetComponent<Animator>();
    }

    public void idleCheck()
    {
        if (!enemyMovement.alerted)
        {
            if (waitCounter > 0.01f)
            {
                waitCounter -= Time.deltaTime;
            }
            else
            {
                idle();
            }
        }
    }

    private void idle()
    {
        Vector3 curWaypointPos = waypoints[currentWaypoint].transform.position;
        wRPos = transform.position.x - curWaypointPos.x;
        if (Vector2.Distance(curWaypointPos, transform.position) < .01f)
        {        
            animator.SetBool("Idling", true);
            waitCounter = waitTime;
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
        else
        {
            animator.SetBool("Idling", false);
            transform.position = Vector2.MoveTowards(transform.position, curWaypointPos, Time.deltaTime * speed);
        }

        flipCheck();
    }

    private void flipCheck()
    {
        if (enemyMovement.isFacingRight && wRPos > 0f || !enemyMovement.isFacingRight && wRPos < 0f)
        {
            Debug.Log("check");
            enemyMovement.isFacingRight = !enemyMovement.isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
