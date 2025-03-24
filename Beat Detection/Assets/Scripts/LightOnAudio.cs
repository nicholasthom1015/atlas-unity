using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightOnAudio : MonoBehaviour
{
    public int _band; // The frequency band to use (0-7)
    public float _minIntensity = 0f; // Minimum light intensity
    public float _maxIntensity = 5f; // Maximum light intensity

    private Light _light;

    void Start()
    {
        _light = GetComponent<Light>();
    }

    void Update()
    {
        if (_band >= 0 && _band < AudioPeer._audioBandBuffer.Length)
        {
            _light.intensity = Mathf.Lerp(_minIntensity, _maxIntensity, AudioPeer._audioBandBuffer[_band]);
        }
    }
}
