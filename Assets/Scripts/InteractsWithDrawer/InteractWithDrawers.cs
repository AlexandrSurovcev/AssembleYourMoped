using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithDrawers : Sounds
{
    public float interactionDistance = 7f;
    public Camera mainCam;
    
    public AudioClip audioOpen;
    public AudioClip audioClose;
    private bool isOpen = false;
    [SerializeField] private KeyCode interactionKey = KeyCode.Mouse0;
    void Update()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, interactionDistance))
        {
            if(hit.collider.CompareTag("Drawers"))
            {
                if(Input.GetKeyUp(interactionKey)){
                    if(isOpen){
                        isOpen = false;
                        Debug.Log("false");
                    }
                    else isOpen = true;
                    hit.transform.GetComponent<Animator>().SetBool("open", isOpen);
                    PlaySound(hit.transform.GetComponent<AudioSource>(), audioOpen);
                }
            }
        }
    }
}
