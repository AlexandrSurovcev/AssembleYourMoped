using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractionWithObjects : Sounds
{
    [SerializeField] private float speedOfRotation = 8000.0f;
    [SerializeField] private int forceOfDrop = 100;
    [SerializeField] private int maxRayDistance = 3;
    [SerializeField] private int maxRayAssemblDistance = 2;
    [SerializeField] private KeyCode dragKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode dropWithForceKey = KeyCode.Mouse1;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform pickUpSocket;
    [SerializeField] private LayerMask defaultPlayerMask;
    [SerializeField] private LayerMask assemblLayerMask;
    [SerializeField] private LayerMask draggableLayerMask;
    [SerializeField] private GameObject assemblUI;

    public GameObject draggableObject;
    public AudioClip audioDisassembl;
    private Rigidbody rbOfDraggableObject;
    private Transform assemblObject;

    void Update()
    {
        HandleObjectInteraction();
    }

    private void HandleObjectInteraction()
    {
        assemblUI.SetActive(false);

        if (draggableObject == null)
        {
            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit, maxRayDistance, defaultPlayerMask))
            {
                TryPickOrDisassembleObject(hit);
            }
        }
        else
        {
            RotateObjectBasedOnInput();
            CheckAndHandleAssembling();
            CheckDropButtons();
        }
    }

    private void TryPickOrDisassembleObject(RaycastHit hit)
    {
        if (hit.transform.CompareTag("Draggable") && Input.GetKeyUp(dragKey))
        {
            PrepareForDrag(hit);
            DragObject();
        }
        else if (hit.transform.CompareTag("Disassembl"))
        {
            if(!hit.transform.GetComponent<Assembled>().isAssembled && hit.transform.GetComponent<Assembled>().needSpinBolts) DisassembleObject(hit);
            else if(!hit.transform.GetComponent<Assembled>().needSpinBolts) DisassembleObject(hit);
        }
    }

    private void RotateObjectBasedOnInput()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        pickUpSocket.Rotate(scrollWheel * speedOfRotation * Time.deltaTime, 0, 0);
    }

    private void CheckAndHandleAssembling()
    {
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit, maxRayAssemblDistance, assemblLayerMask))
        {
            HandleAssembling(hit);
        }
    }

    private void HandleAssembling(RaycastHit hit)
    {
        GameObject assemblObject = draggableObject.GetComponent<Assembled>()?.assemblingObject;
        if (assemblObject != null && hit.transform == assemblObject.transform)
        {
            GetComponent<Assembl>().AssemblParts(hit, draggableObject);
            GetComponent<Assembl>().enabled = true;
        }
        else
        {
            GetComponent<Assembl>().enabled = false;
        }
    }

    private void CheckDropButtons()
    {
        if (Input.GetKeyUp(dragKey))
        {
            DropObject();
        }
        if (Input.GetKeyUp(dropWithForceKey))
        {
            DropObjectWithForce();
        }
    }

    private void DragObject()
    {
        draggableObject.transform.SetParent(null);
        pickUpSocket.position = draggableObject.transform.localPosition;
        draggableObject.transform.SetParent(pickUpSocket, true);
        draggableObject.GetComponent<Draggable>().PrepareForDrag();
        rbOfDraggableObject.isKinematic = true;
    }

    private void DropObject()
    {
        ResetObjectParentAndPhysics();
        draggableObject.GetComponent<Draggable>().PrepareForDrop();
        ClearReferences();
    }

    private void DropObjectWithForce()
    {
        ResetObjectParentAndPhysics();
        draggableObject.GetComponent<Draggable>().DropWithForce(playerCamera.forward, forceOfDrop);
        ClearReferences();
    }

    private void PrepareForDrag(RaycastHit hit)
    {
        draggableObject = hit.transform.gameObject;
        rbOfDraggableObject = draggableObject.GetComponent<Rigidbody>();
    }

    private void DisassembleObject(RaycastHit hit)
    {
        if(Input.GetKey(dropWithForceKey)){
            GameObject[] bolts = hit.transform.GetComponent<Assembled>().bolts;
            foreach (var bolt in bolts)
            {
                bolt.GetComponent<MeshRenderer>().enabled = false;
                bolt.GetComponent<Bolts>().isAssembled = false;
            }
            if(!hit.transform.GetComponent<Assembled>().needSpinBolts){
                hit.transform.GetComponent<Assembled>().isAssembled = false;
            }
            hit.transform.SetParent(null);
            hit.transform.GetComponent<Rigidbody>().isKinematic = false;
            hit.transform.GetComponent<MeshCollider>().isTrigger = false;
            hit.transform.GetComponent<Rigidbody>().useGravity = true;
            hit.transform.tag = "Draggable";
            GameObject _assembled = hit.transform.GetComponent<Assembled>().assemblingObject;
            _assembled.GetComponent<BoxCollider>().enabled = true;
            PlaySound(transform.GetComponent<AudioSource>(), audioDisassembl);
        }
    }

    private void ResetObjectParentAndPhysics()
    {
        draggableObject.transform.SetParent(null);
        rbOfDraggableObject.isKinematic = false;
    }

    private void ClearReferences()
    {
        draggableObject = null;
        rbOfDraggableObject = null;
    }
    
}
