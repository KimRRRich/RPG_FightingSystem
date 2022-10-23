using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Audio
{
    private AudioSource audioSource;

    public Player_Audio(AudioSource audioSource)
    {
        this.audioSource = audioSource;
        Debug.Log("Player_Audio");
    }


    //播放指定的音效
    public void PlayAudio(AudioClip audioClip)
    {
        if(audioClip!=null)
        audioSource.PlayOneShot(audioClip);
    }
}
