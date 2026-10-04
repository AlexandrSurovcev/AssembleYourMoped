using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnAnimation : Sounds
{
    public AudioSource audioSource;
    public AudioClip enterAudioClip;
    public bool isEntered = false;
    // Start is called before the first frame update
    void Start()
    {
        isEntered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        GetComponent<Animator>().SetBool("entered", isEntered);
        
    }
    public void BtnClick(){
        GetComponent<Transform>().localScale = new Vector3(1, 1,1);
    }
    public void BtnEnter(){
        //GetComponent<Animator>().SetBool("entered", true);
        isEntered = true;
        PlaySound(audioSource, enterAudioClip);
    }
    public void BtnExit(){
        //GetComponent<Animator>().SetBool("entered", false);
        isEntered = false;
    }
}
