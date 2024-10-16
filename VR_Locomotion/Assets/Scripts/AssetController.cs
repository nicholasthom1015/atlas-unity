using UnityEngine;
using UnityEngine.AI;

public class AssetController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float speed = 3.5f; // Speed of the asset
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed; // Set the NavMeshAgent speed
    }

    void Update()
    {
        if (player != null)
        {
            // Check if the player is within a certain range
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer > agent.stoppingDistance) // Move only if not close enough
            {
                agent.SetDestination(player.position);
            }
        }
    }
}
