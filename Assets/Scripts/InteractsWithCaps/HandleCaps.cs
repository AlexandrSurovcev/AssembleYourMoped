using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCaps : Sounds
{
    [SerializeField] KeyCode interactionKey = KeyCode.Mouse0;
    [SerializeField] GameObject FuelNeck;
    [SerializeField] AudioClip openAudio;
    [SerializeField] AudioClip closeAudio;
    public float fuelLevelValue = 5.5f;
    public float maxFuelLevelValue;
    public bool isOpened=false;
    public float angleSwitch = 90f;
    float rotZ = 0;
    public GameObject _spinnedObject;
    [SerializeField] private float  _speedOfRotation = 8000.0f;
    public void CheckSpinCap(){
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if(Input.GetAxis("Mouse ScrollWheel")>0){
            PlaySound(GetComponent<AudioSource>(),openAudio);
        }else if(Input.GetAxis("Mouse ScrollWheel")<0){
            PlaySound(GetComponent<AudioSource>(),closeAudio);
        }

        rotZ +=(scrollWheel * _speedOfRotation * Time.deltaTime);
        if(rotZ < -100){
            rotZ = -100;
            _spinnedObject.SetActive(false);
            isOpened = true;
            _spinnedObject.transform.Rotate(0,0,0);
        }
        else if(rotZ > 0){
            rotZ = 0;
            _spinnedObject.transform.Rotate(0,0,0);
        }
        else {
            _spinnedObject.transform.Rotate(0,0,scrollWheel * _speedOfRotation * Time.deltaTime);
            
        }
        if(rotZ > -100){
            _spinnedObject.SetActive(true);
            isOpened = false;
        }
        FuelNeck.SetActive(isOpened);
    }
    public void OpenCloseCap(){
        if(Input.GetKeyUp(interactionKey)){
            if(isOpened){
                PlaySound(GetComponent<AudioSource>(),closeAudio);
                isOpened = false;
                transform.Rotate(0,angleSwitch,0);
            }
            else {
                PlaySound(GetComponent<AudioSource>(),openAudio);
                isOpened = true;
                transform.Rotate(0,-angleSwitch,0);
            }
        }
    }
}
