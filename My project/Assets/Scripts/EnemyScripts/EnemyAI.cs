using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float updateRate;

    private bool alerted;

    // Pathing AI
    private Seeker seeker;
    private Rigidbody2D rb;

    private Path path;

    [SerializeField] private float speed = 300f;
    [SerializeField] private ForceMode2D fMode;

    [HideInInspector]
    private bool pathIsEnded = false;

    [SerializeField] private float nextWaypointDistance = 3;

    private int currentWaypoint = 0;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        seeker.StartPath(this.transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());
    }

    IEnumerator UpdatePath()
    {
        if (target != null)
        {
            seeker.StartPath(this.transform.position, target.position, OnPathComplete);

            yield return new WaitForSeconds(1f / updateRate);
            StartCoroutine(UpdatePath());
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public void chaseCheck(bool alerted)
    {
        this.alerted = alerted;
    }

    private void FixedUpdate()
    {
        if (!alerted)
        {
            return;
        }

        if (target == null)
        {
            return;
        }

        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
            {
                return;
            }

            pathIsEnded = true;
            return;
        }

        pathIsEnded = false;

        Vector2 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;

        rb.AddForce(dir, fMode);

        float dist = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

        if (dist < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }
}
