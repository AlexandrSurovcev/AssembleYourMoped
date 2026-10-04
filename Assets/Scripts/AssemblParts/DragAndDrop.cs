using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private const string Tag = "Draggable";
    [SerializeField] private int _speedOfDrag = 5;
    [SerializeField] private int _forceDrop = 100;
    [SerializeField] private KeyCode _dragKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode _dropWithForceKey = KeyCode.Mouse1;
    [SerializeField] private int MaxRayDistance = 3;
    [SerializeField] private Transform _playerCamera;
    [SerializeField] private Transform _pickUpSocket;
    [SerializeField] private LayerMask _defaultPlayerMask;
    //[SerializeField] private Transform cube;
    public GameObject _draggableObject;
    private Rigidbody _rbOfDraggableObject;
    //
    public Transform _secondobject;
    

    void Update()
    {
        Debug.DrawRay(_playerCamera.position,_playerCamera.forward, Color.red);
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
            }

        }
        else
        {
            //
            RaycastHit hit1;
            if(Physics.Raycast(_playerCamera.position, _playerCamera.forward, out hit1, MaxRayDistance, _defaultPlayerMask))
            {
                if (hit1.transform.CompareTag(Tag))
                {
                    Debug.Log("see second draggable");
                    PrepareForBuild(hit1);
                    CheckBuild();
                }
            }
            else CheckDropButton();
            //
            CheckDropWithForceButton();
        }
        
    }


    //
    private void CheckBuild(){
        if(Input.GetKeyUp(_dragKey)){
            ToItem();
        }
    }
    private void ToItem(){
        _draggableObject.transform.SetParent(_secondobject.transform,true);
        Vector3 localPosition = new Vector3(0,1,0);
        _draggableObject.transform.localPosition = localPosition;
        _rbOfDraggableObject.isKinematic = true;
        _secondobject = null;
        _draggableObject = null;
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
        _draggableObject.GetComponent<Draggable>().DropWithForce(_playerCamera.forward,_forceDrop);

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
    private void PrepareForBuild(RaycastHit hit){
        _secondobject = hit.transform.gameObject.GetComponent<Transform>();
    }
}
