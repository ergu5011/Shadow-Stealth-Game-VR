using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Movement variables
    NavMeshAgent agent;
    [SerializeField]
    private Transform[] waypoints;
    private int waypointIndex;
    Vector3 target;

    // Field of view variables
    public float radius;
    [Range(0,360)]
    public float angle;
    public GameObject playerRef;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();

        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target) < 1)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }

        if (canSeePlayer)
            Debug.Log("See ya");
    }

    // Updates target waypoint when the AI has reached its current target
    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;

        agent.SetDestination(target);
    }

    // Sets the first waypoint as the next waypoint when the AI has reached the last waypoint
    void IterateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }

    // Checks for player five times every second instead of every frame
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    // Checks if player is within field of view
    void FieldOfViewCheck()
    {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeCheck.Length != 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

    void DetectingPlayer()
    {

    }

    void PlayerDetected()
    {

    }
}
