using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    [SerializeField] GameObject Riga13;
    public float fuelLose=50;
    void Update()
    {
        if(Riga13.GetComponent<CheckEngineAssembly>().IsFuel){
            if(Riga13.GetComponent<BicycleVehicle>().MotorStarted){
                float turnovers = Riga13.GetComponent<BicycleVehicle>().CurrentTurnover;
                if(turnovers < 0.6f){
                    turnovers = 0.6f;
                }
                GetComponent<HandleCaps>().fuelLevelValue -= Time.deltaTime/fuelLose *turnovers;
            }
        }
        
    }
}
