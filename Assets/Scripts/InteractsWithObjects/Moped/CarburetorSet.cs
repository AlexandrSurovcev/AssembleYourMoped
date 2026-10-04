using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarburetorSet : MonoBehaviour
{
    // Start is called before the first frame update 36 mixtureBolt
    [SerializeField] Transform mixtureBolt;
    [SerializeField] Transform holostieBolt;
    public float currentMixtureSettings;
    public float currentHolostieSettings;
    void Start()
    {
        int startSetMixture = Random.Range(0, 100);
        int startSetHolostie = Random.Range(0, 20);
        mixtureBolt.GetComponent<Bolts>()._currentCountSpin = startSetMixture;
        holostieBolt.GetComponent<Bolts>()._currentCountSpin = startSetHolostie;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSet();
    }
    void UpdateSet(){
        currentMixtureSettings = mixtureBolt.GetComponent<Bolts>()._currentCountSpin/60.0f;
        if(GetComponent<BicycleVehicle>().minTurnover != currentMixtureSettings){
            if(currentMixtureSettings > 0.4f){
                GetComponent<BicycleVehicle>().minTurnover = currentMixtureSettings;
                GetComponent<BicycleVehicle>().CurrentTurnover = currentMixtureSettings;
            }
            
            if(mixtureBolt.GetComponent<Bolts>()._currentCountSpin >24.0f){
                GetComponent<BicycleVehicle>().motorForce = 1080/mixtureBolt.GetComponent<Bolts>()._currentCountSpin;
                GetComponent<BicycleVehicle>().TurnoverPlusOnGaz = 1620.0f/mixtureBolt.GetComponent<Bolts>()._currentCountSpin;
            }
            if(currentMixtureSettings > 0.45f) GetComponent<MotorSound>().defaultPitch = currentMixtureSettings - 0.1f;
        }
        currentHolostieSettings = 0.011f/holostieBolt.GetComponent<Bolts>()._currentCountSpin;
        if(GetComponent<BicycleVehicle>().TurnoverMinusOnHolostie!=currentHolostieSettings){
            if(holostieBolt.GetComponent<Bolts>()._currentCountSpin >0) GetComponent<BicycleVehicle>().TurnoverMinusOnHolostie = currentHolostieSettings;
        }
    }
}
