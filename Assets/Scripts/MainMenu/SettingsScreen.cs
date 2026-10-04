using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject settingsCanvas;
    [SerializeField] GameObject inputSettingsCanvas;
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropDown;
    public Slider sliderQuality;
    public Slider sliderVolume;
    void Start (){
        resolutionDropDown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;
        for(int i = 0; i<resolutions.Length;i++){
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) currentResolutionIndex = i;
        }
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.RefreshShownValue();
        LoadSettings(currentResolutionIndex);
    }
    public void SetResolution(int resolutionIndex){
        Resolution resolution= resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height,Screen.fullScreen);
    }
    public void SetQuality(float qualityIndex){
        QualitySettings.SetQualityLevel(System.Convert.ToInt32(qualityIndex));
    }
    public void SetVolume(float volumeIndex){
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volumeIndex) *20);
    }

    
    public void BackToMain(){
        mainCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
    }
    public void SaveSettings(){
        PlayerPrefs.SetInt("QualitySettingPreference",System.Convert.ToInt32(sliderQuality.value));
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropDown.value);
        float volume;
        audioMixer.GetFloat("MasterVolume", out volume);
        PlayerPrefs.SetFloat("Volume", volume);
        mainCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
    }
    public void LoadSettings(int currentResolutionIndex){
        if(PlayerPrefs.HasKey("QualitySettingPreference")){
            sliderQuality.value = PlayerPrefs.GetInt("QualitySettingPreference");
        }
        else sliderQuality.value = 5;
        if(PlayerPrefs.HasKey("ResolutionPreference")){
            resolutionDropDown.value = PlayerPrefs.GetInt("ResolutionPreference");
        }
        else resolutionDropDown.value = currentResolutionIndex;
        if(PlayerPrefs.HasKey("Volume")){
            sliderVolume.value = MathF.Pow(10,PlayerPrefs.GetFloat("Volume")/20);
        }
        else sliderVolume.value = 1;

    }
    public void InputSettings(){
        settingsCanvas.SetActive(false);
        inputSettingsCanvas.SetActive(true);
    }
}
