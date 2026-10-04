using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelGunDistance : MonoBehaviour
{
    [SerializeField] GameObject fuelGun;
    private void OnTriggerStay(Collider other){
        if(other.CompareTag("Player")){
            if(other.GetComponent<Tools>().currentTool == fuelGun){
                other.GetComponent<Tools>().setFuelPlace();
            } 
        }
    }
}
