using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Seeker))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float updateRate;

    private bool noticesPlayer = false;

    private bool isFacingRight;

    [SerializeField] GoblinWaypoint gw;

    private bool pulledFromGW = false;

    private float dirX;

    [SerializeField] private float noticeDistance;

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

    void OnPathComplete (Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }

        dirX = target.position.x - transform.position.x;

        if (dirX < noticeDistance)
        {
            noticesPlayer = true;
        }

        if (!noticesPlayer)
        {
            return;
        }

        if (!pulledFromGW)
        {
            isFacingRight = gw.facingRight();
            pulledFromGW = true;
        }

        Flip();

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

        Vector2 dir = ( path.vectorPath[currentWaypoint] - transform.position ).normalized;
        dir *= speed * Time.fixedDeltaTime;

        rb.AddForce(dir, fMode);

        float dist = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

        if (dist < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }

    public bool notices()
    {
        return noticesPlayer;
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
}
