using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypoint = 0;

    [SerializeField] private float speed = 10f;

    void Update()
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
}
