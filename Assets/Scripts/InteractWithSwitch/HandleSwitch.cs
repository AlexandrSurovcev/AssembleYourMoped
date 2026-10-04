using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleSwitch : Sounds
{
    public Light[] pointLights;
    public AudioClip audioClip;
    public GameObject footrestCollider;
    public float angleSwitch = 90f;
    public bool isEnabled = false;
    public void SwitchOnOff(){
        PlaySound(transform.GetComponent<AudioSource>(),audioClip);
        if(isEnabled){
            isEnabled = false;
            transform.Rotate(0,angleSwitch,0);
        }
        else {
            isEnabled = true;
            transform.Rotate(0,-angleSwitch,0);
        }
        for(int i =0;i<pointLights?.Length;i++){
            pointLights[i].enabled = isEnabled;
        }
    }
    public void CraneOpenClose(){
        if(isEnabled){
            isEnabled = false;
        }
        else {
            isEnabled = true;
        }
        transform.GetComponent<Animator>().SetBool("isOpen", isEnabled);
    }
    public void FootrestOpenClose(){
        SphereCollider[] colliders = footrestCollider.GetComponentsInChildren<SphereCollider>();
        if(isEnabled){
            isEnabled = false;
            transform.Rotate(0,angleSwitch,0);
            
        }
        else {
            isEnabled = true;
            transform.Rotate(0,-angleSwitch,0);
        }
        foreach(SphereCollider sphere in colliders){
            sphere.isTrigger = isEnabled;

        }
    }
}
