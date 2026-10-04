using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithPadlock : Sounds
{
    public Camera mainCam;
    public float interactionDistance = 7f;
    public bool isOpen = false;
    public GameObject padlock;
    public GameObject player;
    public GameObject DoorR;
    public GameObject DoorL;
    public AudioClip audioClosedPadlock;
    public AudioClip audioOpenedPadlock;
    private Animator animator;
    [SerializeField] private KeyCode interactionKey = KeyCode.Mouse0;
    void Update()
    {
        openPadlock();
    }
    void openPadlock(){
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, interactionDistance))
        {
            
                if(hit.collider.CompareTag("Padlock"))
                {
                    if(Input.GetKeyUp(interactionKey)){
                        if(player.transform.GetComponent<InteractWithKeys>().haveKeys){
                            if(!isOpen){
                                isOpen = true;
                                animator = GetComponent<Animator>();
                                AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
                                foreach(AnimationClip clip in clips){
                                    AnimationEvent evt = new AnimationEvent();
                                    evt.time = clip.length;
                                    evt.functionName = "OnAnimationFinish";
                                    clip.AddEvent(evt);
                                }
                                DoorR.GetComponent<HandleGates>().canOpen = true;
                                DoorL.GetComponent<HandleGates>().canOpen = true;
                                GetComponent<Animator>().SetBool("open", isOpen);
                                PlaySound(transform.GetComponent<AudioSource>(), audioOpenedPadlock);
                            }
                        } else PlaySound(transform.GetComponent<AudioSource>(), audioClosedPadlock);
                    }
                }
        }
    }
    public void OnAnimationFinish(){
        Destroy(padlock);
    }
}
