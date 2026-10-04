using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public void PlaySound(AudioSource audioSource, AudioClip audioClip, float volume = 0.5f, float pitchMin = 0.85f, float pitchMax = 1f){
        audioSource.pitch = Random.Range(pitchMin, pitchMax);
        audioSource.PlayOneShot(audioClip, volume);
    }
    public void PlaySounds(AudioSource audioSource, AudioClip audioClip, float volume = 1f){
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
