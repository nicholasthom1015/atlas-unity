using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform[] patrolPoints; // Waypoints for patrolling
    public float speed = 2f; // Patrol speed
    public float chaseSpeed = 4f; // Speed when chasing the player
    public float visionRange = 10f; // Detection range
    public float visionAngle = 45f; // Vision cone angle
    public Transform player; // Reference to player
    public LayerMask obstacleMask; // For line-of-sight checking
    public float patrolRadius = 10f; // Radius around the zombie where it picks a new patrol point
    public int maxAttempts = 10; // Max retries to find a valid patrol point
    public float patrolTimeout = 5f; // Time before picking a new patrol point

    private float patrolTimer = 0f; // Timer to track patrol duration
    private int currentPoint = 0;
    private bool isChasing = false;
    private NavMeshAgent agent;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        GoToNextPatrolPoint();
    }

    void Update()
    {
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
            DetectPlayer();
        }
        if (!isChasing) // Only reset patrol if not chasing
        {
        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolTimeout) // If time exceeds limit
            {
            Debug.Log("Patrol timeout reached! Picking new point.");
            GoToNextPatrolPoint();
            patrolTimer = 0f; // Reset timer
            }
        }
    }

    void Patrol()
    {
        if (agent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
    }

    void GoToNextPatrolPoint()
{
    Vector3 randomPoint = GetValidNavmeshPoint(transform.position, patrolRadius);
    if (randomPoint != Vector3.zero) // Found a valid point
    {
        agent.SetDestination(randomPoint);
        patrolTimer = 0f; // Reset the timer when setting a new patrol point
    }
}

Vector3 GetValidNavmeshPoint(Vector3 origin, float radius)
{
    for (int i = 0; i < maxAttempts; i++) // Try multiple times
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += origin;
        randomDirection.y = origin.y; // Keep it at the same height

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            if (IsReachable(hit.position)) // Ensure AI can walk there
            {
                return hit.position;
            }
        }
    }

    return Vector3.zero; // Return zero if no valid point found
}

bool IsReachable(Vector3 target)
{
    NavMeshPath path = new NavMeshPath();
    agent.CalculatePath(target, path);
    return path.status == NavMeshPathStatus.PathComplete; // Check if the path is valid
}

void OnDrawGizmos()
{
    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(transform.position, patrolRadius);
}

    void DetectPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within range
        if (distanceToPlayer < visionRange)
        {
            // Check angle between zombie's forward direction and player
            float angle = Vector3.Angle(transform.forward, directionToPlayer);
            if (angle < visionAngle)
            {
                // Check if there is a clear line of sight
                if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleMask))
                {
                    isChasing = true;
                    agent.speed = chaseSpeed;
                }
            }
        }
    }

    void ChasePlayer()
    {
        agent.destination = player.position;

        // Stop chasing if the player gets too far
        if (Vector3.Distance(transform.position, player.position) > visionRange * 1.5f)
        {
            isChasing = false;
            agent.speed = speed;
            GoToNextPatrolPoint();
        }
    }
}
