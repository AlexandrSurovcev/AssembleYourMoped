using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycleManager : MonoBehaviour
{
    [Range(0, 1)]
public float TimeOfDay;
public float DayDuration = 30f;
public AudioSource audioSourceOfClock;
public AudioClip audioTik;

public GameObject Arrow1;
public GameObject Arrow2;

public AnimationCurve SunCurve;
public AnimationCurve MoonCurve;
public AnimationCurve SkyboxCurve;

public Material DaySkybox;
public Material NightSkybox;

public ParticleSystem Stars;

public Light Sun;
public Light Moon;

private float sunIntensity;
private float moonIntensity;
public float interval = 1f;

private void Start()
{
    sunIntensity = Sun.intensity;
    moonIntensity = Moon.intensity;
    InvokeRepeating("PlaySound",0,interval);
}

void PlaySound(){
    audioSourceOfClock.PlayOneShot(audioTik);
}
private void Update()
{
    TimeOfDay += Time.deltaTime / DayDuration;
    if (TimeOfDay >= 1) TimeOfDay -= 1;


    Arrow1.transform.Rotate(-360/DayDuration*Time.deltaTime*24, 0, 0);
    Arrow2.transform.Rotate(-360/DayDuration*Time.deltaTime*24/12, 0, 0);

    // Настройки освещения (skybox и основное солнце)
    RenderSettings.skybox.Lerp(NightSkybox, DaySkybox, SkyboxCurve.Evaluate(TimeOfDay));
    RenderSettings.sun = SkyboxCurve.Evaluate(TimeOfDay) > 0.1f ? Sun : Moon;
    DynamicGI.UpdateEnvironment();

    // Прозрачность звёзд
    var mainModule = Stars.main;
    mainModule.startColor = new Color(1, 1, 1, 1 - SkyboxCurve.Evaluate(TimeOfDay));

    // Поворот луны и солнца
    Sun.transform.localRotation = Quaternion.Euler(TimeOfDay * 270f, 160, 0);
    Moon.transform.localRotation = Quaternion.Euler(TimeOfDay * 3 * 270f + 160, 160, 0);

    // Интенсивность свечения луны и солнца
    Sun.intensity = sunIntensity * SunCurve.Evaluate(TimeOfDay);
    Moon.intensity = moonIntensity * MoonCurve.Evaluate(TimeOfDay);
}
}
