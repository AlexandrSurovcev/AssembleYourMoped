using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assembl : Sounds
{
    private const string Tag = "Assembl";
    [SerializeField] private Transform _playerCamera;
    public GameObject Player;
    public GameObject _draggableObject;
    public GameObject assemblUI;
    public AudioClip audioAssembl;
    private Rigidbody _rbOfDraggableObject;
    [SerializeField] private LayerMask _defaultPlayerMask;
    private const string DisassemblTag = "Disassembl";
    [SerializeField] private KeyCode _dragKey = KeyCode.Mouse0;
    //
    private Transform _assemblObject;
    private RaycastHit hit;
    void Update()
    {
        assemblUI.SetActive(false);
        if(_draggableObject.transform.GetComponent<Assembled>()){
            if (hit.transform.CompareTag(Tag))
            {
                PrepareForAssembling(hit);
                GameObject assemblObject = _draggableObject.transform.GetComponent<Assembled>().assemblingObject;
                if(assemblObject!=null && _assemblObject.transform == assemblObject.transform){
                    assemblUI.SetActive(true);
                    CheckAssembl();
                }
            }
        }
    }
    public void AssemblParts(RaycastHit hit1, GameObject draggable){
        hit = hit1;
        _draggableObject = draggable;
        _rbOfDraggableObject = draggable.transform.GetComponent<Rigidbody>();
    }

    private void CheckAssembl(){
        if(Input.GetKeyUp(_dragKey)){
            assemblUI.SetActive(false);
            AssemblingParts();
        }
    }

    private void CheckPlace(){
        GameObject place = _draggableObject.transform.GetComponent<Assembled>()?.haveAssembled;
            if(place!=null){
                
                if(place.transform.GetComponent<Assembled>().isAssembled){
                    GameObject[] bolts = _draggableObject.transform.GetComponent<Assembled>().bolts;
                    for(int i = 0; i < bolts.Length; i ++){
                        bolts[i].GetComponent<MeshRenderer>().enabled = true;
                    }

                    GameObject[] additional = _draggableObject.transform.GetComponent<Assembled>().additionalObjects;
                    for(int i = 0; i < additional.Length; i ++){
                        additional[i].GetComponent<MeshRenderer>().enabled = true;
                    }
                }
            }
            else {
                GameObject[] bolts = _draggableObject.transform.GetComponent<Assembled>().bolts;
                    for(int i = 0; i < bolts.Length; i ++){
                        bolts[i].GetComponent<MeshRenderer>().enabled = true;
                    }
                    GameObject[] additional = _draggableObject.transform.GetComponent<Assembled>().additionalObjects;
                    for(int i = 0; i < additional.Length; i ++){
                        additional[i].GetComponent<MeshRenderer>().enabled = true;
                    }
                _assemblObject.GetComponent<BoxCollider>().enabled = false;
                // GameObject wire = _draggableObject.transform.GetComponent<Assembled>().wire;
                // wire.transform.GetComponent<MeshRenderer>().enabled = true;
            }
    }

    
    private void AssemblingParts(){
        _draggableObject.transform.SetParent(_assemblObject.transform,true);
            Vector3 localPosition = _draggableObject.transform.GetComponent<Assembled>()._position;
            if(!_draggableObject.GetComponent<Assembled>().needSpinBolts){
                _draggableObject.GetComponent<Assembled>().isAssembled = true;
            }
            
            CheckPlace();
            Quaternion rot = _assemblObject.transform.rotation;
            _draggableObject.transform.localPosition = localPosition;
            _draggableObject.transform.rotation = rot;
            _draggableObject.tag = DisassemblTag;
            _draggableObject.layer = 0;
            _rbOfDraggableObject.isKinematic = true;
            _draggableObject.transform.GetComponent<MeshCollider>().isTrigger = true;
            this.transform.GetComponent<InteractionWithObjects>().draggableObject = null;
            this.transform.GetComponent<Assembl>().enabled = false;
            PlaySound(transform.GetComponent<AudioSource>(), audioAssembl);
            _rbOfDraggableObject = null;
            _assemblObject = null;
            _draggableObject = null;
    }
    private void PrepareForAssembling(RaycastHit hit){
        _assemblObject = hit.transform.gameObject.GetComponent<Transform>();
    }
}
