using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour
{
    public int _band;
    public float _startScale = 0.1f;
    public float _scaleMultiplier = 10.0f;
    public float _smoothingFactor = 0.1f; // Adjust this value to control the smoothness

    private float _previousScale;

    void Update()
    {
        // Calculate the target scale based on the frequency band data
        float targetScale = _startScale + AudioPeer._freqBand[_band] * _scaleMultiplier;

        // Smoothly transition to the target scale using a buffer
        float newScale = Mathf.Lerp(_previousScale, targetScale, _smoothingFactor);
        _previousScale = newScale;

        transform.localScale = new Vector3(transform.localScale.x, newScale, transform.localScale.z);
    }
}