using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinWaypoint : MonoBehaviour
{
    public EnemyMovement enemyMovement;
    [SerializeField] private EnemyAI enemyAI;
    private bool isFacingRight = false;
    private float dirX;

    [Header("Waypoint")]
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypoint = 0;

    [SerializeField] private float speed = 10f;

    private Rigidbody2D rb;

    [Header("Enemy Attributes")]
    [SerializeField] private Enemy enemy;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!enemyMovement.alerted)
        {
            Vector3 curWaypointPos = waypoints[currentWaypoint].transform.position;
            if (Vector2.Distance(curWaypointPos, transform.position) < .1f)
            {
                currentWaypoint++;
                if (currentWaypoint >= waypoints.Length)
                {
                    currentWaypoint = 0;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, curWaypointPos, Time.deltaTime * speed);

            dirX = curWaypointPos.x - transform.position.x;

            Flip();
        }
    }

    private void Flip()
    {
        if (isFacingRight && dirX < 0f || !isFacingRight && dirX > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public bool facingRight()
    {
        return isFacingRight;
    }
}
