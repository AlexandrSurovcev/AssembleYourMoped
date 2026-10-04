using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InteractWithCaps : MonoBehaviour
{
    public float fuelLevelValue;
    public float maxFuelLevelValue;
    public GameObject fuelLevel;
    public float interactionDistance = 10f;
    public Camera mainCam;
    public bool isFilling = false;
    
    [SerializeField] private float  _speedOfRotation = 8000.0f;
    void Update()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, interactionDistance))
        {
            if(hit.collider.CompareTag("Caps"))
            {
                hit.collider.GetComponent<HandleCaps>().CheckSpinCap();
                updateFuelLevel(hit.transform);
            }
            else if(hit.collider.CompareTag("CasolineCap")){
                hit.collider.GetComponent<HandleCaps>().OpenCloseCap();
                updateFuelLevel(hit.transform);
            }
            else if(isFilling){
                fuelLevel.SetActive(true);
            }
            else fuelLevel.SetActive(false);
        }
    }
    public void updateFuelLevel(Transform obj){
        fuelLevelValue = obj.GetComponent<HandleCaps>().fuelLevelValue;
        maxFuelLevelValue = obj.GetComponent<HandleCaps>().maxFuelLevelValue;
        if(obj.GetComponent<HandleCaps>().isOpened){
            fuelLevel.SetActive(true);
            var slider = fuelLevel.GetComponent<Slider>();
            slider.value = fuelLevelValue;
            slider.maxValue = maxFuelLevelValue;
        }
        else fuelLevel.SetActive(false);
    }
}
