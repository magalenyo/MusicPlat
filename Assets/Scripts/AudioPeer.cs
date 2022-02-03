using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioPeer : MonoBehaviour
{
    AudioSource audioSource;
    public static float[] samples = new float[512];
    public static float[] freqBand = new float[8];

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
    }

    private void GetSpectrumAudioSource()
    {
        audioSource?.GetSpectrumData(samples, 0, FFTWindow.Blackman);     // transforms 20k (20K hz) samples into 512
    }

    private void MakeFrequencyBands()
    {
        /*
         * 22050/512 = 43hz per sample -> 22050 hz of the song
         * 
         * 20-60hz
         * 60-250hz
         * 250-500hz
         * 500-2000hz
         * 2000-4000hz
         * 4000-6000hz
         * 6000-20000hz
         * 
         * 0 - 2 = 86hz
         * 1 - 4 = 172hz - 87-258
         * 2 - 8 = 344hz - 259-602
         * 3 - 16 = 688hz - 603-1290
         * 4 - 32 = 1376hz - 1291-2666
         * 5 - 64 = 2752hz - 2667-5418
         * 6 - 128 = 5504hz - 5419-10922
         * 7 - 256 = 11008hz - 10923-21930
         * 
         * 510 samples
         */
        int count = 0;

        for(int i = 0; i < 8; ++i) {
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            float average = 0;
            
            if (i == 7)
            {
                sampleCount += 2; // to get to the 22khz spectrum cuz 7 wasnt enough
            }

            for(int j = 0; j < sampleCount; ++j)
            {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= count;
            freqBand[i] = average * 10;
        }

    }
}
