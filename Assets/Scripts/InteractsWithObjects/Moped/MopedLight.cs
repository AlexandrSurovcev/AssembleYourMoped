using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopedLight : Sounds
{
    public Camera mainCam;
    public Light headLight;
    public AudioClip switchSound;
    public AudioSource SwitchAudioSource;
    public GameObject ignitionButton;
    public GameObject lightModeSwitch;
    public GameObject HeadLight;
    private float angleSwitch = 7f;
    private bool isLightning;
    private bool isSwitchedMode;
    private Vector3 targetPosition = new Vector3(0,0,0.00015f);
    private float targetInnerSpotangle = 70f;
    private float targetOutterSpotangle = 90f;
    private float currentInnerSpotAngle;
    private float currentOutterSpotAngle;
    [SerializeField] private KeyCode interactionKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode lightOnOffKey = KeyCode.L;

    void Start(){
        currentInnerSpotAngle = headLight.innerSpotAngle;
        currentOutterSpotAngle = headLight.spotAngle;
    }
    void Update()
    {
        //if(HeadLight.GetComponent<Assembled>().isAssembled){
            CheckLightButton();
            Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.CompareTag("Ignition"))
                {
                    if(Input.GetKeyUp(interactionKey)){
                        //ChangeLightMode();
                        GetComponent<BicycleVehicle>().EngineShutDown();
                        PlaySound(SwitchAudioSource,switchSound);
                        pressButton();
                    }
                }
            }
        //}
        // else{
        //     headLight.enabled = false;
        // }
        if(HeadLight.GetComponent<Assembled>().isAssembled && GetComponent<BicycleVehicle>().MotorStarted == true) headLight.intensity = GetComponent<BicycleVehicle>().CurrentTurnover*20;
        else headLight.intensity = 0;
    }
    void pressButton(){
        ignitionButton.transform.localPosition = ignitionButton.transform.localPosition - targetPosition;
        StartCoroutine(pressedButton());
    }
    IEnumerator pressedButton(){
        yield return new WaitForSeconds(0.3f);
        ignitionButton.transform.localPosition = ignitionButton.transform.localPosition + targetPosition;
    }
    void ChangeLightMode(){
        if(isSwitchedMode){
            isSwitchedMode = false;
            lightModeSwitch.transform.Rotate(0,angleSwitch,0);
            headLight.innerSpotAngle = currentInnerSpotAngle;
            headLight.spotAngle = currentOutterSpotAngle;
        }
        else{
            isSwitchedMode = true;
            lightModeSwitch.transform.Rotate(0,-angleSwitch,0);
            headLight.innerSpotAngle = targetInnerSpotangle;
            headLight.spotAngle = targetOutterSpotangle;
        }

    }

    void CheckLightButton(){
        if(Input.GetKeyUp(lightOnOffKey)){
            ChangeLightMode();
            PlaySound(SwitchAudioSource,switchSound);
            // if(isLightning){
            //     isLightning =false;
            //     lightButton.transform.localPosition = lightButton.transform.localPosition + targetPosition;
            //     PlaySound(SwitchAudioSource,switchSound);
            // }
            // else {
            //     isLightning =true;
            //     lightButton.transform.localPosition = lightButton.transform.localPosition - targetPosition;
            //     PlaySound(SwitchAudioSource,switchSound);
            // }
        }
        //headLight.enabled = isLightning;
    }
}
