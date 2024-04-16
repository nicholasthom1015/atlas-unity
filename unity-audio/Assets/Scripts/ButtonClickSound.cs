using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to the button
        audioSource = GetComponent<AudioSource>();

        // Add a listener to the button's click event
        GetComponent<Button>().onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        // Play the attached audio clip
        audioSource.Play();
    }
}
