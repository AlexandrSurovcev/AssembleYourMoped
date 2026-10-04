using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopedAnimation : MonoBehaviour
{
    public GameObject handleBrake;
    public GameObject handleClutch;
    public GameObject handleGaz;
	[SerializeField] Transform[] Gears;
    public float rotationSpeed = -750;
    BicycleVehicle BicycleVehicle;
    void Start(){
        BicycleVehicle = GetComponent<BicycleVehicle>();
    }
    void Update()
    {
        UpdateEngine();
        if(GetComponent<BicycleVehicle>().enabled==true){
            if(Input.GetKey(KeyCode.Space)){
                handleBrake.GetComponent<Animator>().SetBool("press", true);
            }
            else handleBrake.GetComponent<Animator>().SetBool("press", false);
        
            if(Input.GetKey(KeyCode.LeftShift)){
                handleClutch.GetComponent<Animator>().SetBool("press", true);
            }
            else handleClutch.GetComponent<Animator>().SetBool("press", false);
            
            if(GetComponent<BicycleVehicle>().MotorStarted){
                if(Input.GetKey(KeyCode.W)){
                    handleGaz.GetComponent<Animator>().SetBool("press", true);
                }
                else handleGaz.GetComponent<Animator>().SetBool("press", false);
            }
        }
    }
    private void UpdateEngine(){
		if(BicycleVehicle.MotorStarted){
            if(BicycleVehicle.enabled==false || GetComponent<BicycleVehicle>().verticalInput==0){
                for (int i = 0;i<Gears.Length;i++){
                    if(i!=2){
                        Gears[1].transform.Rotate(Vector3.right,GetComponent<BicycleVehicle>().CurrentTurnover*Time.deltaTime*rotationSpeed);
                        Gears[0].transform.Rotate(Vector3.right,GetComponent<BicycleVehicle>().CurrentTurnover*Time.deltaTime*-rotationSpeed);
                    }
                }
            }
            else if(GetComponent<BicycleVehicle>().verticalInput!=0){
                for (int i = 0;i<Gears.Length;i++){
                    if(i!=0){
                        Gears[i].transform.Rotate(Vector3.right,GetComponent<BicycleVehicle>().CurrentTurnover*Time.deltaTime*rotationSpeed);
                    }
                    else Gears[i].transform.Rotate(Vector3.right,GetComponent<BicycleVehicle>().CurrentTurnover*Time.deltaTime*-rotationSpeed);
                }
            }
        }
        
	}
}
