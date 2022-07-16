using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnCue : MonoBehaviour
{
    public AudioSource audioSource1;
    public AudioSource audioSource2;

    public void PlayAudio1()
    {
        audioSource1.Play();
    }

    public void PlayAudio2()
    {
        audioSource2.Play();
    }
}
