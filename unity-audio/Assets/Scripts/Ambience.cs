using UnityEngine;

public class AmbientAudioController : MonoBehaviour
{
    public AudioSource ambientAudioSource;
    public float maxDistance = 10f; // Maximum distance at which the audio is audible
    public float minVolume = 0.1f; // Minimum volume when the player is at maximum distance
    public float maxVolume = 1f; // Maximum volume when the player is closest to the GameObject

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assuming the player is tagged as "Player"
    }

    void Update()
    {
        // Calculate the distance between the player and the GameObject
        float distance = Vector3.Distance(transform.position, player.position);

        // Calculate the volume based on the distance
        float volume = Mathf.Lerp(minVolume, maxVolume, 1 - Mathf.Clamp01(distance / maxDistance));

        // Set the volume of the ambient audio source
        ambientAudioSource.volume = volume;
    }
}
