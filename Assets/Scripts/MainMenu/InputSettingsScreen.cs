using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputSettingsScreen : MonoBehaviour
{
    [SerializeField] GameObject settingsCanvas;
    [SerializeField] GameObject inputSettingsCanvas;
    [SerializeField] Slider sliderSensivity;
    public float currentSensivity;
    void Start(){
        LoadSettings();
    }
    public void Settings(){
        inputSettingsCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }
    public void SetSensivity(float sensivity){
        currentSensivity = sensivity;
    }
    public void SaveSettings(){
        PlayerPrefs.SetFloat("SensivityLevel", currentSensivity);
        inputSettingsCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }
    public void LoadSettings(){

        if(PlayerPrefs.HasKey("SensivityLevel")){
            sliderSensivity.value = PlayerPrefs.GetFloat("SensivityLevel");
        }
        else sliderSensivity.value = 2;

    }
}
