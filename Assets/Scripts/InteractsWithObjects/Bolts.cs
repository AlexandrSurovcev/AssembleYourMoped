using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolts : Sounds
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material usableMaterial;
    public bool isAssembled;
    public GameObject tool;
    public int countSpin;
    public int _currentCountSpin;
    public AudioSource audioSource;
    public AudioClip audioScrew;
    public AudioClip audioUnScrew;
    public float entranceLength = 0.0003f;
    public int angleSpin = 30;
    void Start()
    {
        if(!isAssembled){
            _currentCountSpin = countSpin;
        }
    }
    public void lightBolts(bool toggle){
        if(toggle){
            setMaterials(transform,usableMaterial);
        }
        else {
            setMaterials(transform,defaultMaterial);
        }
    }

    private void setMaterials(Transform transform, Material material){
        Material[] materials = transform.GetComponent<MeshRenderer>().materials;
            for(int i = 0; i < materials.Length;i++){
                materials[i] = material;
        }
        transform.GetComponent<MeshRenderer>().materials = materials;
    }

    public void CheckSpin(){
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if(scrollWheel>0){
            if(_currentCountSpin > 0){
                _currentCountSpin-=1;
                transform.localPosition = new Vector3(transform.localPosition.x + entranceLength,transform.localPosition.y,transform.localPosition.z);
                PlaySound(audioSource,audioScrew);
                transform.Rotate(-angleSpin,0,0);
            }
            else isAssembled = true;
        }
        
        if(scrollWheel<0){
            if(_currentCountSpin <= countSpin){
                _currentCountSpin+=1;
                transform.localPosition = new Vector3(transform.localPosition.x - entranceLength,transform.localPosition.y,transform.localPosition.z);
                PlaySound(audioSource,audioUnScrew);
                transform.Rotate(angleSpin,0,0);
            }
            else isAssembled = false;
        }
        
    }
}
