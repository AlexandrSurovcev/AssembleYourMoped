using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeatDriver : MonoBehaviour
{
    public TextMeshProUGUI enterTextDriver;
    public GameObject moped;
    public GameObject Player;
    public GameObject playerSound;
    public float rotationSpeed = 2f;
    public bool isPicked = false;
    private bool isDriving = false;
    private Vector3 targetposition;
    public Vector3 targetpositionup;
    public float x;
    public float y;
    

    // Update is called once per frame
    void Update()
    {
        if (isPicked){
            float targetRotation = 0f;
            moped.GetComponent<Rigidbody>().isKinematic = true;
            //moped.transform.position = Vector3.Lerp(moped.transform.position, targetposition,rotationSpeed * Time.deltaTime);
            moped.transform.rotation = Quaternion.Slerp(moped.transform.rotation, Quaternion.Euler(x,y,targetRotation), rotationSpeed * Time.deltaTime);
            if(Quaternion.Angle(moped.transform.rotation,Quaternion.Euler(x,y,targetRotation))< 0.1f){
                isPicked = false;
            }
        }
    }
    private void OnTriggerStay(Collider other){
        if(moped.layer != 6){
            if(other.CompareTag("Player")){
                if(!(Quaternion.Angle(moped.transform.rotation,Quaternion.Euler(x,y,0f))< 0.5f)){
                    x = moped.transform.rotation.eulerAngles.x;
                    y = moped.transform.rotation.eulerAngles.y;
                    isPicked = true;
                    targetposition = moped.transform.position + targetpositionup;
                } 
                else{
                    if(!isDriving){
                        enterTextDriver.text = "Enter, чтобы сесть";
                        enterTextDriver.enabled = true;
                    }
                    CheckDrivingMode();
                }
            }
        }
             
    }
    void CheckDrivingMode(){
        if(Input.GetKeyUp(KeyCode.Return)){
            Debug.Log("DrivingMode");
            DrivingMode();
        }
    }

    void DrivingMode(){
        if(!isDriving){
            enterTextDriver.enabled = false;
            moped.GetComponent<Rigidbody>().isKinematic = false;
            moped.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
            moped.transform.GetComponent<BicycleVehicle>().enabled = true;
            Player.transform.SetParent(moped.transform, true);
            moped.transform.tag = "Moped";
            isDriving = true;
        }
        else{
            moped.GetComponent<Rigidbody>().freezeRotation = false;
            moped.transform.GetComponent<BicycleVehicle>().enabled = false;
            Player.transform.SetParent(null);
            moped.transform.tag = "Draggable";
            isDriving = false;
        }
        ChangeMode();
    }
    void ChangeMode(){
        Player.transform.GetComponent<Rigidbody>().isKinematic = isDriving;
        Player.transform.GetComponent<CapsuleCollider>().isTrigger = isDriving;
        Player.transform.GetComponent<FirstPersonMovement>().enabled = !isDriving;
        playerSound.transform.GetComponent<FirstPersonAudio>().enabled = !isDriving;
        transform.GetComponent<SeatDriver>().enabled = !isDriving;
        moped.transform.GetComponent<Draggable>().enabled = !isDriving;
        moped.transform.GetComponent<MopedLight>().enabled = isDriving;
    }
    void OnTriggerExit(){
        isPicked = false;
        moped.GetComponent<Rigidbody>().isKinematic = false;
        enterTextDriver.enabled = false;
    }
}
