using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractWithDoors : Sounds
{
    public float interactionDistance = 7f;
    public Camera mainCam;
    [SerializeField] private KeyCode interactionKey = KeyCode.Mouse0;
    void Update()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, interactionDistance))
        {
            if(hit.collider.CompareTag("Doors"))
            {
                if(Input.GetKeyUp(interactionKey)){
                    bool haveKeys = transform.GetComponent<InteractWithKeys>().haveKeys;
                    hit.collider.transform.GetComponent<HandleDoor>().OpenCloseDoor(haveKeys);
                }
            }
            if(hit.collider.CompareTag("Gates"))
            {
                hit.collider.transform.GetComponent<HandleGates>().openCloseGates();
            }
        }
    }
}
