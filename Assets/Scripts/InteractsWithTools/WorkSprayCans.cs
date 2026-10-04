using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WorkSprayCans : MonoBehaviour
{
    [SerializeField] GameObject[] sprayCans;
    public AudioClip workSprayCanAudio;
    public int id;
    public AudioSource audioSource;
    [SerializeField] private int maxRayDistance = 3;
    [SerializeField] private int maxRayAssemblDistance = 2;
    [SerializeField] private KeyCode dragKey = KeyCode.Mouse0;
    [SerializeField] private Transform playerCamera;
    [SerializeField] Transform Frame;
    [SerializeField] Transform Riga;
    GameObject currentSpray;
    public float time;
    public bool audioOn;
    
    
    
    void Update()
    {
        CheckIdSpray();
        ColorParts();
    }
    void CheckIdSpray(){
        currentSpray = GetComponent<Tools>().currentTool;
        for (int i = 0; i< sprayCans.Length;i++){
            if(sprayCans[i] == currentSpray){
                id = i;
            }
        }
    }
    void PaintPart(Transform part){
        Material[] materials = part.GetComponent<MeshRenderer>().materials;
        if(materials.Length > 0){
            materials[part.GetComponent<Painted>().materialId] = part.GetComponent<Painted>()?.colors[id];
            part.GetComponent<MeshRenderer>().materials = materials;
        }
    }
    void ColorParts(){
        
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit, maxRayDistance))
        {
            if (hit.transform.GetComponent<Painted>() || hit.transform==Riga)
            {
                
                
                if(Input.GetKey(dragKey)){
                    if(!audioOn){
                        audioSource.clip = workSprayCanAudio;
                        audioSource.Play();
                        currentSpray.GetComponent<UsableTools>().spray.Play();
                        audioOn = true;
                    }
                    time += Time.deltaTime;
                    if(time > 2){
                        if(hit.transform == Riga){
                            PaintPart(Frame);
                        }
                        else PaintPart(hit.transform);
                        time = 0;
                        currentSpray.GetComponent<UsableTools>().spray.Stop();
                        audioSource.Stop();
                        audioOn = false;
                    }
                }
                else {
                    currentSpray.GetComponent<UsableTools>().spray.Stop();
                    audioSource.Stop();
                    audioOn = false;
                }
                
            }
            else {
                currentSpray.GetComponent<UsableTools>().spray.Stop();
                audioSource.Stop();
                audioOn = false;
            }
        } 
    }
}
