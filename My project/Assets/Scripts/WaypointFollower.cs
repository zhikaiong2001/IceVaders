using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [Header ("Waypoint")]
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypoint = 0;

    [SerializeField] private float speed = 10f;

    [Header ("Knockback")]
    [SerializeField] private float KBForceHor;
    [SerializeField] private float KBForceVer;
    [SerializeField] private float KBCounter;
    [SerializeField] private float KBTotalTime;
    [SerializeField] private bool KnockFromRight;
    private bool isFacingRight = false;

    private Rigidbody2D rb;

    [Header("Enemy Attributes")]
    [SerializeField] private Enemy enemy;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (KBCounter <= .01f && !enemy.deathStatus())
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
        }
        else if (!enemy.deathStatus())
        {
            if (KnockFromRight)
            {
                rb.velocity = new Vector2(-KBForceHor, KBForceVer);

                if (!isFacingRight)
                {
                    isFacingRight = !isFacingRight;
                    Vector3 localScale = transform.localScale;
                    localScale.x *= -1f;
                    transform.localScale = localScale;
                }
            }
            else
            {
                rb.velocity = new Vector2(KBForceHor, KBForceVer);

                if (isFacingRight)
                {
                    isFacingRight = !isFacingRight;
                    Vector3 localScale = transform.localScale;
                    localScale.x *= -1f;
                    transform.localScale = localScale;
                }
            }

            KBCounter -= Time.deltaTime;
        }
    }


    // Knockback Methods
    public void setKBCounter(float time)
    {
        KBCounter = time;
    }

    public float getKBTotalTime()
    {
        return KBTotalTime;
    }

    public void setKBRight(bool right)
    {
        KnockFromRight = right;
    }
}
