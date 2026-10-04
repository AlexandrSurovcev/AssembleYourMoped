using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDoor : Sounds
{
    public GameObject Door;
    public AudioClip audioOpen;
    public AudioClip audioClose;
    public AudioClip audioClosedDoor;
    public AudioClip audioOpenedDoor;
    private bool isOpen = false;
    public bool isClosedDoor = false;
    public void OpenCloseDoor(bool haveKeys){
        if(!isClosedDoor){
            if(isOpen){
                isOpen = false;
                PlaySound(GetComponent<AudioSource>(), audioClose);
            }
            else{
                isOpen = true;
                PlaySound(GetComponent<AudioSource>(), audioOpen);
            }
            Door.GetComponent<Animator>().SetBool("open", isOpen);
        }
        else {
            if(haveKeys){
                PlaySound(GetComponent<AudioSource>(), audioOpenedDoor);
                isClosedDoor = false;
            }
            else PlaySound(GetComponent<AudioSource>(), audioClosedDoor);
        }
    }
}
