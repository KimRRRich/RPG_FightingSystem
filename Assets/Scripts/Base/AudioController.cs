using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController
{
    private AudioSource audioSource;

    public AudioController(AudioSource audioSource)
    {
        this.audioSource = audioSource;
        Debug.Log("Player_Audio");
    }


    //����ָ������Ч
    public void PlayAudio(AudioClip audioClip)
    {
        if(audioClip!=null)
        audioSource.PlayOneShot(audioClip);
    }
}
