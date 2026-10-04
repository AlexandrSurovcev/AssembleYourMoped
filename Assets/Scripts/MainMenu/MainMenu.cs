using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;
using UnityEngine.Rendering;

public class MainMenu : Sounds
{
    public Button button;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject settingsCanvas;
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    void Start(){
        resolutions = Screen.resolutions;
        StartSettings();
    }
    public void StartGame(){
        SceneId.Instance.SetIdScene(2);
        SceneManager.LoadScene(3);
    }
    public void Quit(){
        Application.Quit();
    }
    public void Settings(){
        mainCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }
    public void StartSettings(){
        if(PlayerPrefs.HasKey("QualitySettingPreference")){
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualitySettingPreference"));
        }
        if(PlayerPrefs.HasKey("ResolutionPreference")){
            Resolution resolution = resolutions[PlayerPrefs.GetInt("ResolutionPreference")];
            Screen.SetResolution(resolution.width, resolution.height,Screen.fullScreen);
        }
        if(PlayerPrefs.HasKey("Volume")){
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(MathF.Pow(10,PlayerPrefs.GetFloat("Volume")/20)) *20);
        }
        if(PlayerPrefs.HasKey("SensivityLevel")){
            Debug.Log(PlayerPrefs.GetFloat("SensivityLevel"));
        }
    }
}
