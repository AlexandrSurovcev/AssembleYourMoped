using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillingFuel : MonoBehaviour
{
    [SerializeField] Transform casoline;
    [SerializeField] Transform fuelTank;
    public bool isFillingFuel = false;
    void Update(){
        FillingAudio();
    }
    private void OnTriggerStay(Collider other){
        if(casoline.GetComponent<HandleCaps>().isOpened){
            if(other.CompareTag("CasolineCap")){
                fuelTank.GetComponent<InteractWithCaps>().isFilling = true;
                FillUpFuel(Time.deltaTime/2);
            }
            else {
                fuelTank.GetComponent<InteractWithCaps>().isFilling = false;
            }
        }
    }

    void FillingAudio(){
        if(fuelTank.GetComponent<InteractWithCaps>().isFilling){
            if(!isFillingFuel){
                GetComponent<AudioSource>().Play();
                isFillingFuel = true;
            }
        }
        else {
            stopFillingAudio();
            isFillingFuel = false;
        }
    }
    void stopFillingAudio(){
        if(isFillingFuel){
            GetComponent<AudioSource>().Stop();
        }
    }
    void FillUpFuel(float fuelvalue){
        if(fuelTank.GetComponent<HandleCaps>().fuelLevelValue < fuelTank.GetComponent<HandleCaps>().maxFuelLevelValue){
            if(casoline.GetComponent<HandleCaps>().fuelLevelValue > 0){
                fuelTank.GetComponent<HandleCaps>().fuelLevelValue += fuelvalue;
                casoline.GetComponent<HandleCaps>().fuelLevelValue -= fuelvalue;
                fuelTank.GetComponent<InteractWithCaps>().updateFuelLevel(fuelTank); 
            }
            else stopFillingAudio();
        }
        else stopFillingAudio();
    }
}
