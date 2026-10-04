using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScreen : MonoBehaviour
{
    
    void Start(){
        StartCoroutine(LoadAsync(SceneId.Instance.GetIdScene()));
    }
    public GameObject loadWrench;
    IEnumerator LoadAsync(int SceneIdMeneger){
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(SceneIdMeneger);
        loadAsync.allowSceneActivation = false;
        while(!loadAsync.isDone){
            loadWrench.transform.Rotate(-Time.deltaTime*180, 0, 0);
            if(loadAsync.progress >= .9f && !loadAsync.allowSceneActivation){
                yield return new WaitForSeconds(2.2f);
                loadAsync.allowSceneActivation=true;
            }
            yield return null;
        }
    }
}
