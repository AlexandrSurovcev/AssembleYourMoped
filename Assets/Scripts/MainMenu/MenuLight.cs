using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLight : MonoBehaviour
{
    public float intensity;
    bool isEnable = false;
    
    void Update()
    {
        if(!isEnable){
            float timeToOff = Random.Range(2, 5);
            StartCoroutine(OffLightning(timeToOff));
            isEnable = true;
        }
    }
    IEnumerator OffLightning(float r){
        yield return new WaitForSeconds(r);
        GetComponent<Light>().intensity = 0;
        float timeToOn = Random.Range(0.3f, 0.9f);
        StartCoroutine(OnLightning(timeToOn));
    }
    IEnumerator OnLightning(float r){
        yield return new WaitForSeconds(r);
        GetComponent<Light>().intensity = intensity;
        isEnable = false;
    }
}
