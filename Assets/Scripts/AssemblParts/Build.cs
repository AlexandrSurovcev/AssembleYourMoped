using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Build : MonoBehaviour
{
    private const string Tag = "Draggable";
    private const string DisassemblTag = "Disassembl";
    [SerializeField] private int _speedOfDrag = 5;
    [SerializeField] private int _forceOfDrop = 100;
    [SerializeField] private int MaxRayDistance = 3;
    [SerializeField] private int MaxRayAssemblDistance = 2;
    [SerializeField] private KeyCode _dragKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode _dropWithForceKey = KeyCode.Mouse1;
    [SerializeField] private Transform _playerCamera;
    [SerializeField] private Transform _pickUpSocket;
    [SerializeField] private LayerMask _defaultPlayerMask;
    [SerializeField] private LayerMask _assPlayerMask;
    public GameObject assemblUI;
    public GameObject _draggableObject;
    public Rigidbody _rbOfDraggableObject;
    //
    public Transform _assemblObject;
    

    void Update()
    {
        assemblUI.SetActive(false);
        if(_draggableObject==null)
        {
            RaycastHit hit;
            if(Physics.Raycast(_playerCamera.position, _playerCamera.forward, out hit, MaxRayDistance, _defaultPlayerMask))
            {
                if (hit.transform.CompareTag(Tag))
                {
                    Debug.Log("see draggable");
                    if(Input.GetKey(_dragKey))
                    {
                        PrepareForDrag(hit);
                    }
                }
                else if(hit.transform.CompareTag(DisassemblTag) && !hit.transform.GetComponent<Assembled>().isAssembled){
                    Debug.Log("see dissasembl");
                    if(Input.GetKey(_dropWithForceKey))
                    {
                        Disassembl(hit);
                    }
                }
            }
        }
        else
        {
            
            RaycastHit hit1;
            if(Physics.Raycast(_playerCamera.position, _playerCamera.forward, out hit1, MaxRayAssemblDistance, _assPlayerMask))
            {
                Debug.Log("e");
                this.transform.GetComponent<Assembl>().AssemblParts(hit1,_draggableObject);
                this.transform.GetComponent<Assembl>().enabled = true;
            //     if(_draggableObject.transform.GetComponent<Assembling>()){
            //         if (hit1.transform.CompareTag("Assembl"))
            //         {
            //             Debug.Log("see second draggable");
            //             PrepareForAssembling(hit1);
            //             GameObject assemblObject = _draggableObject.transform.GetComponent<Assembling>().GetObject();
            //             if(assemblObject!=null && _assemblObject.transform == assemblObject.transform){
            //                 assemblUI.SetActive(true);
            //                 CheckAssembl();
            //             }
            //         }
            //     }
            }
            else{
                this.transform.GetComponent<Assembl>().enabled = false;
                //assemblUI.SetActive(false);
                CheckDropButton();
                CheckDropWithForceButton();
            }
        }
        
    }


    
    // private void CheckAssembl(){
    //     if(Input.GetKeyUp(_dragKey)){
    //         assemblUI.SetActive(false);
    //         AssemblingParts();
    //     }
    // }
    // private void AssemblingParts(){
    //     _draggableObject.transform.SetParent(_assemblObject.transform,true);
    //         Vector3 localPosition = _draggableObject.transform.GetComponent<Assembling>().GetPosition();


    //         GameObject[] bolts = _draggableObject.transform.GetComponent<Assembling>().bolts;
    //         for(int i = 0; i < bolts.Length; i ++){
    //             bolts[i].GetComponent<MeshRenderer>().enabled = true;
    //         }

    //         Quaternion rot = _assemblObject.transform.rotation;
    //         _draggableObject.transform.localPosition = localPosition;
    //         _draggableObject.transform.rotation = rot;
    //         _draggableObject.tag = DisassemblTag;
    //         _draggableObject.layer = 0;
    //         _rbOfDraggableObject.isKinematic = true;
    //         _draggableObject.transform.GetComponent<BoxCollider>().isTrigger = true;
    //         _rbOfDraggableObject = null;
    //         _assemblObject = null;
    //         _draggableObject = null;
    // }

    private void Disassembl(RaycastHit hit){
        GameObject[] bolts = hit.transform.GetComponent<Assembled>().bolts;
            for(int i = 0; i < bolts.Length; i ++){
                bolts[i].GetComponent<MeshRenderer>().enabled = false;
                bolts[i].GetComponent<Bolts>().isAssembled = false;
            }
        hit.transform.SetParent(null);
        hit.transform.GetComponent<Rigidbody>().isKinematic = false;
        hit.transform.GetComponent<MeshCollider>().isTrigger = false;
        hit.transform.GetComponent<Rigidbody>().useGravity = true;
        hit.transform.tag = Tag;
    }


    private void CheckDropButton()
    {
        if (Input.GetKeyUp(_dragKey))
        {
            Drop();
        }
    }
    private void CheckDropWithForceButton()
    {
        if (Input.GetKeyDown(_dropWithForceKey))
        {
            DropWithForce();
        }
    }
    private void FixedUpdate()
    {
        if(Input.GetKey(_dragKey) && _draggableObject != null)Drag();
    }
    private void Drag()
    {
        Vector3 dragDirection = _pickUpSocket.position - _draggableObject.transform.position;
        _rbOfDraggableObject.velocity = dragDirection * _speedOfDrag;
    }
    public void Drop()
    {
        _draggableObject.GetComponent<Draggable>().PrepareForDrop();
        _draggableObject = null;
        _rbOfDraggableObject = null;
    }

    private void DropWithForce()
    {
        _draggableObject.GetComponent<Draggable>().DropWithForce(_playerCamera.forward,_forceOfDrop);

        _draggableObject = null;
        _rbOfDraggableObject = null;
    }

    private void PrepareForDrag(RaycastHit hit)
    {
        _draggableObject = hit.transform.gameObject;
        _rbOfDraggableObject = _draggableObject.GetComponent<Rigidbody>();
        _draggableObject.GetComponent<Draggable>().PrepareForDrag();
    }

    //
    // private void PrepareForAssembling(RaycastHit hit){
    //     _assemblObject = hit.transform.gameObject.GetComponent<Transform>();
    // }
}
