using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleGates : Sounds
{
    public float openAngle = 90f;
    public bool canOpen = false;
    
    public AudioClip audioOpen;
    public AudioClip audioClose;
    [SerializeField] private KeyCode interactionKey = KeyCode.Mouse0;
    
    [SerializeField] private float  _speedOfRotation = 10f;
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    void Start(){
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(0,openAngle,0);
    }
    public void openCloseGates(){
        if(canOpen){
            if(Input.GetKey(interactionKey)){
                isOpen = true;
                PlaySound(transform.GetComponent<AudioSource>(), audioOpen);
            }
            else if(Input.GetKey(KeyCode.Mouse1))
            {
                isOpen = false;
                PlaySound(transform.GetComponent<AudioSource>(), audioClose);
            }
            if(isOpen){
                transform.rotation = Quaternion.Slerp(transform.rotation, openRotation,Time.deltaTime * _speedOfRotation);   
            }
            else{
                transform.rotation = Quaternion.Slerp(transform.rotation,closedRotation,Time.deltaTime * _speedOfRotation);    
            }
        }
    }
}
