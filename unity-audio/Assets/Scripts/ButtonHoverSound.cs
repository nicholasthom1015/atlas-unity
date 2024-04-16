using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverSound : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to the button
        audioSource = GetComponent<AudioSource>();
    }

    // This method is called when the mouse pointer enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Play the attached audio clip
        if(audioSource != null)
            audioSource.Play();
    }
}
