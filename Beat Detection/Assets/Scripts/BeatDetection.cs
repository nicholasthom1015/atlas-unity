using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatDetection : MonoBehaviour
{
    public AudioSource audioSource;
    public Material material;
    public float sensitivity = 5f;
    public float smoothTime = 0.1f;

    private float beatIntensity;
    private float smoothedBeat;

    void Update()
    {
        float[] spectrumData = new float[256];
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

        // Get the average amplitude of low frequencies (bass)
        float sum = 0;
        for (int i = 0; i < 20; i++) sum += spectrumData[i]; // Adjust range for different frequency bands
        beatIntensity = sum * sensitivity;

        // Smooth the beat response
        smoothedBeat = Mathf.Lerp(smoothedBeat, beatIntensity, smoothTime);

        // Send data to shader
        material.SetFloat("_BeatIntensity", smoothedBeat);
    }
}
