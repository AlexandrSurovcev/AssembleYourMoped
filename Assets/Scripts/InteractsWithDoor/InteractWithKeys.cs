using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithKeys : Sounds
{
    public Camera mainCam;
    public AudioClip audioTakeKeys;
    public float interactionDistance = 7f;
    public bool haveKeys = false;
    [SerializeField] private KeyCode interactionKey = KeyCode.Mouse0;
    void Update()
    {
        takeKeys();
    }
    void takeKeys(){
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, interactionDistance))
        {
            if(hit.collider.CompareTag("Keys"))
            {
                if(Input.GetKeyUp(interactionKey)){
                    haveKeys = true;
                    Destroy(hit.collider.gameObject);
                    PlaySound(GetComponent<AudioSource>(), audioTakeKeys, volume: 0.3f);
                }
            }
        }
    }
}
