using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssembledParts : MonoBehaviour
{
    public bool assembledParts = true;
    private GameObject assembledPart;
    private const string DisassemblTag = "Disassembl";

    void Start()
    {
        PrepareForAssembledParts();
        if(assembledParts){
            transform.SetParent(assembledPart.transform,true);
            Vector3 localPosition = transform.GetComponent<Assembled>()._position;
            GetComponent<Assembled>().isAssembled = true;
            Quaternion rot = assembledPart.transform.rotation;
            transform.localPosition = localPosition;
            transform.rotation = rot;
            tag = DisassemblTag;
            gameObject.layer = 0;
            GetComponent<MeshCollider>().isTrigger = true;
            GetComponent<Rigidbody>().isKinematic = true;
            GameObject[] bolts = GetComponent<Assembled>().bolts;
            for(int i = 0; i < bolts.Length; i ++){
                bolts[i].GetComponent<MeshRenderer>().enabled = true;
                SpinBolts(bolts[i].transform);
                
            }
            GameObject[] additionalObjects = GetComponent<Assembled>().additionalObjects;
            for(int i = 0; i < additionalObjects.Length; i ++){
                additionalObjects[i].GetComponent<MeshRenderer>().enabled = true;
            }
            Debug.Log(tag);
        }
    }
    void SpinBolts(Transform bolt){
        Vector3 vector3 = new Vector3(bolt.localPosition.x + bolt.GetComponent<Bolts>().entranceLength * bolt.GetComponent<Bolts>().countSpin,bolt.localPosition.y,bolt.localPosition.z);
        bolt.localPosition = vector3;
        bolt.GetComponent<Bolts>().isAssembled = true;
        bolt.GetComponent<Bolts>()._currentCountSpin = 0;
    }
    void PrepareForAssembledParts(){
        assembledPart = GetComponent<Assembled>().assemblingObject;
    }
}
