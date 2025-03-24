using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParamCube : MonoBehaviour
{
    public int _band;
    public float _startScale = 0.1f;
    public float _scaleMultiplier = 10.0f;
    public float _smoothingFactor = 0.1f; // Adjust this value to control the smoothness
    Material _material;

    //private float _previousScale;
    public bool _useBuffer;

    void Start()
    {
        _material = GetComponent<MeshRenderer> ().materials [0];
    }

    void Update()
{
    float targetScale;
    Color targetColor;

    if (_useBuffer)
    {
        targetScale = (AudioPeer._bandBuffer[_band] * _scaleMultiplier) + _startScale;
        targetColor = new Color(AudioPeer._audioBandBuffer[_band], AudioPeer._audioBandBuffer[_band], AudioPeer._audioBandBuffer[_band]);
    }
    else
    {
        targetScale = (AudioPeer._freqBand[_band] * _scaleMultiplier) + _startScale;
        targetColor = new Color(AudioPeer._audioBand[_band], AudioPeer._audioBand[_band], AudioPeer._audioBand[_band]);
    }

    // Smoothly scale the object
    transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, targetScale, _smoothingFactor), transform.localScale.z);

    // Smoothly interpolate emission color
    // _material.SetColor("_EmissionColor", Color.Lerp(_material.GetColor("_EmissionColor"), targetColor, Time.deltaTime * 5f));
    // _material.EnableKeyword("_EMISSION");  // Ensure emission is applied
}

    // void Update()
    // {
    //     // Calculate the target scale based on the frequency band data
    //     if (_useBuffer)
    //     {
    //         transform.localScale = new Vector3 (transform.localScale.x, (AudioPeer._bandBuffer [_band] * _scaleMultiplier) + _startScale, transform.localScale.z);
    //         Color _color = new Color (AudioPeer._audioBandBuffer [_band], AudioPeer._audioBandBuffer [_band], AudioPeer._audioBandBuffer [_band]);
    //         _material.SetColor ("_EmissionColor", _color);

    //     }

    //     if (!_useBuffer)
    //     {
    //         transform.localScale = new Vector3(transform.localScale.x, (AudioPeer._freqBand [_band] * _scaleMultiplier) + _startScale, transform.localScale.z);
    //         Color _color = new Color (AudioPeer._audioBand [_band], AudioPeer._audioBand [_band], AudioPeer._audioBand [_band]);
    //         _material.SetColor ("_EmissionColor", _color);
    //     }

    //     Debug.Log($"Band {_band}: {AudioPeer._bandBuffer[_band]}");

    // }
}

