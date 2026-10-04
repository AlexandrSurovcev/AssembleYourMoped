using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorSound : MonoBehaviour
{
    public float audioPitch = 0.55f;
    AudioSource audioSource;
    public float defaultPitch = 0.55f;
    public float maxPitch = 2.14f;
    private float pitchFromMotor;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = audioPitch;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.GetComponent<BicycleVehicle>().MotorStarted){
            pitchFromMotor = BicycleVehicle.bv.CurrentTurnover;
            if(pitchFromMotor < defaultPitch){
                audioSource.pitch = defaultPitch;
            }
            else if(pitchFromMotor > maxPitch){
                audioSource.pitch = maxPitch;
            }
            else audioSource.pitch = pitchFromMotor;
        }
    }
}
