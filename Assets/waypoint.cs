using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoint : MonoBehaviour
{
    [Header("Waypoint going toward")]
    public float minDistanceToReachWaypoint = 5;

    public waypoint[] nextwaypoint;
}
