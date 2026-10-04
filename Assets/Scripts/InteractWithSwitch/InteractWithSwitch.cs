using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithSwitch : MonoBehaviour 
{
     public float interactionDistance = 3f; 
    [SerializeField] KeyCode interactionKey = KeyCode.Mouse0;
    public Camera mainCam;
    void Update()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, interactionDistance))
        {
            if(hit.collider.CompareTag("Switch"))
            {
                
                if(Input.GetButtonDown("InteractionKey")){
                    hit.collider.transform.GetComponent<HandleSwitch>()?.SwitchOnOff();
                }
            }
            if(hit.collider.CompareTag("FuelCrane")){
                if(Input.GetKeyUp(interactionKey)){
                    hit.collider.transform.GetComponent<HandleSwitch>()?.CraneOpenClose();
                }
            }
            if(hit.collider.CompareTag("Footrest")){
                if(Input.GetKeyUp(interactionKey)){
                    hit.collider.transform.GetComponent<HandleSwitch>()?.FootrestOpenClose();
                }
            }
        }
        
    }
    
}
