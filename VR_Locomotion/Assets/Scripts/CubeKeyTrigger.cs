using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeKeyTrigger : MonoBehaviour
{
    public Animator Exit; // Assign in Inspector
    private bool hasPlayed = false; // Flag to track animation state

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CubeKey") && !hasPlayed)
        {
            Exit.SetTrigger("Exit"); // Trigger animation
            hasPlayed = true; // Mark as played
        }
    }
}

