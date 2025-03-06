using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioPeer : MonoBehaviour
{
    AudioSource _audioSource;
    public static float[] _samples = new float[512];
    public static float [] _freqBand = new float[8];
    public static float [] _bandBuffer = new float[8];
    private float [] _bufferDecrease = new float[8];

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudtioSource ();
        MakeFreqenyBands ();
        BandBuffer ();
    }

    void BandBuffer()
    {
        for (int g = 0; g < 8; ++g)
            {
                if (_freqBand [g] > _bandBuffer [g])
                    {
                        _bandBuffer[g] = _freqBand [g];
                        _bufferDecrease [g] = 0.005f;
                    }
                if (_freqBand [g] < _bandBuffer [g])
                    {
                        _bandBuffer [g] -= _bufferDecrease [g];
                        _bufferDecrease [g] *= 1.2f;
                    }
            }
    }

    void GetSpectrumAudtioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    void MakeFreqenyBands()
    {
        /*
        * 22050 / 512 = 43hertz per sample
        * 
        * 20 - 60 hertz
        * 60 - 250 hertz
        * 250 - 500 hertz
        * 500 - 2000 hertz
        * 2000 - 4000 hertz
        * 4000 - 6000 hertz
        * 6000 - 20000 hertz

        * 0 - 2 = 86 hertz
        * 1 - 4 = 172 hertz - 87-258
        * 2 - 8 = 344 hertz - 259-602
        * 3 - 16 = 688 hertz - 603-1290
        * 4 - 32 = 1376 hertz - 1291-2666
        * 5 - 64 = 2752 hertz - 2667-5418
        * 6 - 128 = 5504 hertz - 5419-10922
        * 7 - 256 = 11008 hertz - 10923-21930
        * 510 
        */

        int count = 0;

    for (int i = 0; i < 8; i++)
    {
        float average = 0;
        int sampleCount = (int)Mathf.Pow(2, i) * 2;

        if (i == 7)
        {
            sampleCount += 2;
        }

        for (int j = 0; j < sampleCount; j++)
        {
            average += _samples[count] * _samples[count]; // Square the samples for energy calculation
            count++;
        }

        average /= sampleCount;
        average = Mathf.Sqrt(average); // Take the square root to get RMS amplitude

        _freqBand[i] = average * 10;
    }
}
}
