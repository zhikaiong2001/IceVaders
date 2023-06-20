using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdling : MonoBehaviour
{
    private EnemyMovement enemyMovement;

    [SerializeField] private EnemyAI enemyAI;
    public bool isFacingRight { get; private set; }

    [Header("Waypoint")]
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypoint = 0;

    [Header("Attributes")]
    [SerializeField] private float speed;
    public float waitTime;

    private void Start()
    {
        isFacingRight = true;
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void idleCheck()
    {
        if (!enemyMovement.alerted)
        {
            idle();
        }
    }

    IEnumerator idle()
    {
        Vector3 curWaypointPos = waypoints[currentWaypoint].transform.position;
        if (Vector2.Distance(curWaypointPos, transform.position) < .01f)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
            yield return new WaitForSeconds(waitTime);
        }
        transform.position = Vector2.MoveTowards(transform.position, curWaypointPos, Time.deltaTime * speed);

        flip();
    }

    private void flip()
    {
        if (isFacingRight && enemyMovement.rPos < 0f || !isFacingRight && enemyMovement.rPos > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
