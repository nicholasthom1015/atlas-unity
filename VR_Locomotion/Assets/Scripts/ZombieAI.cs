using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform[] patrolPoints;  // Array of waypoints for patrol
    public Transform player;          // Reference to the player
    public float sightRange = 10f;    // How far the zombie can see
    public float fieldOfView = 45f;   // Field of view in degrees
    public float chaseTime = 5f;      // How long before zombie gives up chasing
    public LayerMask playerLayer;     // Layer mask to detect the player
    public LayerMask obstructionLayer; // Layer mask to detect obstacles

    private NavMeshAgent agent;
    private int currentPatrolIndex = 0;
    private float lostSightTimer = 0f;
    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
        }
        
        CheckForPlayer();
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        currentPatrolIndex = Random.Range(0, patrolPoints.Length);
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
        
        if (!CanSeePlayer())
        {
            lostSightTimer += Time.deltaTime;
            if (lostSightTimer >= chaseTime)
            {
                isChasing = false;
                lostSightTimer = 0f;
                GoToNextPatrolPoint();
            }
        }
        else
        {
            lostSightTimer = 0f; // Reset timer if player is in sight
        }
    }

    void CheckForPlayer()
    {
        if (CanSeePlayer())
        {
            isChasing = true;
        }
    }

    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (angle < fieldOfView && Vector3.Distance(transform.position, player.position) <= sightRange)
        {
            if (!Physics.Linecast(transform.position, player.position, obstructionLayer))
            {
                return true;
            }
        }

        return false;
    }
}
