using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAudio : MonoBehaviour
{
    public AudioClip[] birdClips;
    public AudioClip[] cocoretsClips;
    public GameObject DayCycle;
    public GameObject FuelLamp;
    AudioSource audioSource;
    public AudioSource CocoretAudioSource;
    bool audioPlayed;
    bool audioCocPlayed;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }
    void Update()
    {
        if(DayCycle.GetComponent<DayCycleManager>().TimeOfDay >0.045f && DayCycle.GetComponent<DayCycleManager>().TimeOfDay <0.6f){
            
            int audioClip = Random.Range(0, 3);
            if(!audioPlayed){
                audioSource.clip = birdClips[audioClip];
                int interval = Random.Range(9, 14);
                StartCoroutine(StopAudio(interval));
                audioSource.Play();
                audioPlayed = true;
            }
        }
        if((DayCycle.GetComponent<DayCycleManager>().TimeOfDay >0.96f && DayCycle.GetComponent<DayCycleManager>().TimeOfDay <1f) ||(DayCycle.GetComponent<DayCycleManager>().TimeOfDay >0f && DayCycle.GetComponent<DayCycleManager>().TimeOfDay <0.03f)){
            
            int audioClip = Random.Range(0, 2);
            if(!audioCocPlayed){
                CocoretAudioSource.clip = cocoretsClips[audioClip];
                int interval = Random.Range(3, 9);
                StartCoroutine(StopCocAudio(interval));
                CocoretAudioSource.Play();
                audioCocPlayed = true;
            }
        }
        if(DayCycle.GetComponent<DayCycleManager>().TimeOfDay > 0.65f || DayCycle.GetComponent<DayCycleManager>().TimeOfDay < 0.04f){
            FuelLamp.SetActive(true);
        }
        else FuelLamp.SetActive(false);
    }
    IEnumerator StopCocAudio(int interval){
        yield return new WaitForSeconds(interval);
        CocoretAudioSource.Stop();
        audioCocPlayed = false;
    }
    IEnumerator StopAudio(int interval){
        yield return new WaitForSeconds(interval);
        audioSource.Stop();
        audioPlayed = false;
    }
}
