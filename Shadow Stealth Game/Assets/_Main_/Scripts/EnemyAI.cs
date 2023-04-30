using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private PlayerStatistics stats;

    // Movement variables
    NavMeshAgent agent;
    [SerializeField]
    private Transform[] waypoints;
    private int waypointIndex;
    Vector3 target;

    private float speed;
    private float startSpeed;

    // Field of view variables
    public float radius;
    [Range(0,360)]
    public float angle;
    public GameObject playerRef;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;

    private float detection = 0;
    [SerializeField]
    private bool isStatic;
    [SerializeField]
    private GameObject audioComponent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (isStatic == false)
            UpdateDestination();

        //playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());

        //alertAud = GetComponent<AudioSource>();

        startSpeed = agent.speed;
    }

    void Update()
    {
        agent.speed = speed;

        if (stats.Visibility == 0)
            canSeePlayer = false;

        if (!canSeePlayer) // Patroll mode
        {
            if (Vector3.Distance(transform.position, target) < 1 && isStatic == false)
            {
                IterateWaypointIndex();
                UpdateDestination();
            }

            speed = startSpeed;

            audioComponent.SetActive(false);

            detection -= 1 * Time.deltaTime;
        }
        else if (stats.Visibility > 0) // Alert mode
        {
            speed = 0f;
            DetectingPlayer();
            audioComponent.SetActive(true);
        }

        if (detection <= 0)
            detection = 0;
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
        transform.LookAt(playerRef.transform);

        detection += stats.Visibility * 1f * Time.deltaTime;

        //alertAud.Play();

        if (detection >= 3)
            stats.isCaught = true;
    }
}
