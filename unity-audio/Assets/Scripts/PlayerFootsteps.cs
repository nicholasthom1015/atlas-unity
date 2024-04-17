using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource footstepSound;
    public AudioClip footstepsRunningGrass;
    public AudioClip footstepsRunningRock;
    public LayerMask ground;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if the player is on the ground
        if (IsGrounded())
        {
            // Check if the player is running
            if (Input.GetKey(KeyCode.W)) // Assuming W key is for running forward
            {
                // Check the material of the ground
                if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.1f, ground))
                {
                    if (hit.collider.CompareTag("Grass"))
                    {
                        Debug.Log("grass");
                        PlayFootstepSound(footstepsRunningGrass);
                    }
                    else if (hit.collider.CompareTag("Stone"))
                    {
                        PlayFootstepSound(footstepsRunningRock);
                    }
                }
            }
        }
    }

    private bool IsGrounded()
    {
        // Check if the player is grounded (adjust the value based on your player's height)
        return Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }

    private void PlayFootstepSound(AudioClip clip)
    {
        // Ensure the footstep sound is not already playing
        if (!footstepSound.isPlaying)
        {
            footstepSound.clip = clip;
            footstepSound.Play();
        }
    }
}
