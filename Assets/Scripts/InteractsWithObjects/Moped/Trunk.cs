using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Trunk : MonoBehaviour
{
    [SerializeField] GameObject trunkCollider;
    [SerializeField] Transform trunk;
    private void OnTriggerStay(Collider other){
        if(other.GetComponent<Draggable>() && other.GetComponent<Rigidbody>().isKinematic == false && other.gameObject.layer == 0){
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.SetParent(transform, true);
        }    
    }
}
