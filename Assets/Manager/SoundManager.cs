using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SoundManager : BaseManager
{

    public AudioSource audioSource;

    public int position = 0;
    public int samplerate = 44100;
    public float frequency = 440;


    public override BaseManager Init ()
    {
        Debug.Log ("INIT");

        audioSource = new GameObject ("SoundManager_Audio")
            .Add<AudioListener>()
            .AddComponent<AudioSource> ();

        AudioClip myClip = AudioClip.Create ("MySinusoid", samplerate * 2, 1, samplerate, true, OnAudioRead, OnAudioSetPosition);

        audioSource.clip = myClip;
        audioSource.Play ();

        return this;
    }
    void OnAudioRead (float[] data)
    {
        int count = 0;
        while(count < data.Length)
        {
            data[count] = Mathf.Sin (2 * Mathf.PI * frequency * position / samplerate);
            position++;
            count++;
        }
    }

    void OnAudioSetPosition (int newPosition)
    {
        position = newPosition;
    }
}
