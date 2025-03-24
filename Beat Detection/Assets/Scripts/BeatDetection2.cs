using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatDetector2 : MonoBehaviour
{
    public AudioSource audioSource;
    public Material material; // The shader material
    public float sensitivity = 1.5f; // Adjust for different audio sources

    private float[] samples = new float[512]; // Audio samples
    private float beatStrength;

    void Update()
    {
        if (audioSource.isPlaying)
        {
            AnalyzeAudio();
            material.SetFloat("_BeatStrength", beatStrength);
        }
    }

    void AnalyzeAudio()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
        float sum = 0f;

        // Analyze low frequencies (bass, usually between index 0-10)
        for (int i = 0; i < 10; i++)
        {
            sum += samples[i];
        }

        beatStrength = Mathf.Clamp(sum * sensitivity, 0f, 1f);
    }
}
