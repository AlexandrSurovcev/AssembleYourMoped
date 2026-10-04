using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneId : MonoBehaviour
{
    public static SceneId Instance{get;set;}
    
    int idScene;
    public void Awake(){
        if(Instance == null)
            Instance = this;
        else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    

    public void SetIdScene(int id){
        idScene = id;
    }
    public int GetIdScene(){
        return idScene;
    }
}
