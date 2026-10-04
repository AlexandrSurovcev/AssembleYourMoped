using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Triggers : MonoBehaviour
{
    [SerializeField] GameObject playerCamera;
    [SerializeField] GameObject loadingCanvas;
    [SerializeField] GameObject[] Canvases;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Moped")){
            playerCamera.GetComponent<FirstPersonLook>().enabled = false;
            for(int i = 0;i<Canvases.Length;i++){
                Canvases[i].SetActive(false);
            }
            playerCamera.GetComponent<Animator>().SetBool("load", true);
            SceneId.Instance.SetIdScene(1);
            StartCoroutine(StartGameOnTrigger());
        }     
    }
    IEnumerator StartGameOnTrigger(){
        yield return new WaitForSeconds(2);
        loadingCanvas.SetActive(true);
        SceneManager.LoadScene(3);
    }
}
