using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseTrainingMenu : MonoBehaviour
{
    [SerializeField] GameObject playerCamera;
    public bool PauseGame;
    public GameObject PauseGameMenu;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(PauseGame){
                Resume();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else{
                Pause();
                Cursor.lockState = CursorLockMode.None;
            }
            playerCamera.GetComponent<FirstPersonLook>().enabled = !PauseGame;
        }
        Cursor.visible = PauseGame;
    }
    public void Resume(){
        PauseGameMenu.SetActive(false);
        Time.timeScale = 1.0f;
        PauseGame = false;
    }
    public void GoToPlay(){
        Time.timeScale = 1.0f;
        SceneId.Instance.SetIdScene(1);
        SceneManager.LoadScene(3);
    }
    public void Pause(){
        PauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        PauseGame = true;
    }
    public void GoToMainMenu(){
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
