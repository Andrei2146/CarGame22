using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class carai : MonoBehaviour
{
    carsscripts carsscripts;
    public enum AIMode { followPlayer, followWaypoints };
    [Header("AI settings")]
    public AIMode aiMode; 

    Vector3 targetPosition = Vector3.zero; 
    Transform targetTransform = null;

    waypoint currentWaypoint = null;
    waypoint[] allWayPoints; 

     void Awake()
    {
        carsscripts = GetComponent<carsscripts>();
        allWayPoints = FindObjectsOfType<waypoint>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;
       switch (aiMode)
        {
            case AIMode.followPlayer:
                FollowPlayer();
                break;

                case AIMode.followWaypoints:
                FollowWayPoints();
                break;
        }

        inputVector.x = TurnTowardTarget();
        inputVector.y = 1.0f;

        carsscripts.SetInputVector(inputVector);
        
    }

    void FollowWayPoints()
    {
        if (currentWaypoint == null)
       
            currentWaypoint = FindClosestWayPoint();

        if (currentWaypoint == null)
        {
            targetPosition = currentWaypoint.transform.position;

            float distanceToWayPoint = (targetPosition - transform.position).magnitude;

            if (distanceToWayPoint <= currentWaypoint.minDistanceToReachWaypoint)
            {
                currentWaypoint = currentWaypoint.nextwaypoint[Random.Range(0, currentWaypoint.nextwaypoint.Length)];
            }
        }
    }
    
    waypoint FindClosestWayPoint ()
    {
        return allWayPoints
        .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
            .FirstOrDefault();
    }
    void FollowPlayer()
    {
        if (targetTransform == null)
            targetTransform = GameObject.FindGameObjectWithTag("player").transform;

        if (targetTransform != null)
            targetPosition = targetTransform.position;
    }

    float TurnTowardTarget()
    {
        Vector2 vectorToTarget = targetPosition - transform.position;
        vectorToTarget.Normalize();

        float angleToTarget = Vector2.SignedAngle(transform.up, vectorToTarget);
        angleToTarget *= -1;

        float steerAmount = angleToTarget / 45.0f;

        steerAmount = Mathf.Clamp(steerAmount, -1.0f, 1.0f);

        return steerAmount;
    }
}
