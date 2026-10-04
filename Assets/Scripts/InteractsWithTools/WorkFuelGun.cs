using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkFuelGun : Sounds
{
    // Start is called before the first frame update
    public Camera mainCam;
    public float interactionDistance = 3f;
    private Transform container;
    public AudioClip gasStationOnAudio;
    public AudioClip gasStationOffAudio;
    public AudioClip gasStationWorkingAudio;
    public AudioClip fuelFillingAudio;
    [SerializeField] Transform gasStation;
    [SerializeField] Transform arrow;
    AudioSource personAudio;
    bool isWorking = false;
    void Start()
    {
        personAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, interactionDistance))
        {
            if(hit.collider.CompareTag("Caps") || hit.collider.CompareTag("CasolineCap"))
            {
                container = hit.collider.transform;
                if(container.GetComponent<HandleCaps>().isOpened){
                    if(container.GetComponent<HandleCaps>().fuelLevelValue < container.GetComponent<HandleCaps>().maxFuelLevelValue){
                        container.GetComponent<HandleCaps>().fuelLevelValue += Time.deltaTime/1.5f;
                        arrow.Rotate(0, -Time.deltaTime *2.5f,0);
                        if(!isWorking){
                            gasStation.GetComponent<AudioSource>().clip = gasStationOnAudio;
                            gasStation.GetComponent<AudioSource>().Play();
                            StartCoroutine(StartFillingFuel());
                            isWorking = true;
                        }
                        
                    }
                    else if(isWorking)StopFilligFuelAudio();
                }
                else if(isWorking)StopFilligFuelAudio();
            }
            else if(isWorking)StopFilligFuelAudio();
        }
    }
    public void StopFilligFuelAudio(){
        PlaySound(gasStation.GetComponent<AudioSource>(),gasStationOffAudio);
        StartCoroutine(StopFillingFuel());
        isWorking = false;
        personAudio.GetComponent<AudioSource>().Stop();
    }
    IEnumerator StopFillingFuel(){
        yield return new WaitForSeconds(0.5f);
        gasStation.GetComponent<AudioSource>().Stop();
    }

    IEnumerator StartFillingFuel(){
        yield return new WaitForSeconds(0.3f);
        personAudio.GetComponent<AudioSource>().clip = fuelFillingAudio;
        personAudio.GetComponent<AudioSource>().Play();
        gasStation.GetComponent<AudioSource>().Stop();
        gasStation.GetComponent<AudioSource>().clip = gasStationWorkingAudio;
        gasStation.GetComponent<AudioSource>().Play();
    }
}
