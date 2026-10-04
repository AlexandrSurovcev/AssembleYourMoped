using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class workTools : MonoBehaviour
{
    private const string Tag = "Bolts";
    [SerializeField] private int MaxRayDistance = 3;
    [SerializeField] private Transform _playerCamera;
    [SerializeField] private LayerMask layerMask;
    public GameObject person;
    private RaycastHit hit;

    
    void Update()
    {
        if(hit.collider != null){
            hit.collider.GetComponent<Bolts>()?.lightBolts(false);
            if(hit.collider.GetComponent<Bolts>()?.tool == person.transform.GetComponent<Tools>().currentTool && person.transform.GetComponent<InteractionWithObjects>().draggableObject == null){
                hit.transform.GetComponent<Bolts>().CheckSpin();
            }
        }
        if(Physics.Raycast(_playerCamera.position, _playerCamera.forward, out hit, MaxRayDistance, layerMask))
        {
            if(hit.collider.GetComponent<Bolts>()?.tool == person.transform.GetComponent<Tools>().currentTool && person.transform.GetComponent<InteractionWithObjects>().draggableObject == null){
                hit.collider.GetComponent<Bolts>()?.lightBolts(true);
            }
        }
    }
    
    
}
