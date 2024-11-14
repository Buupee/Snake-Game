using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    public GameObject SettingsMenu;
    public bool PauseGame;
    public GameObject pauseGameMenu;
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(PauseGame){
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Pause(){
         pauseGameMenu.SetActive(true);
         Time.timeScale=0f;
         PauseGame = true;
    }
    public void Resume(){
         pauseGameMenu.SetActive(false);
         SettingsMenu.SetActive(false);
         Time.timeScale=1f;
         PauseGame= false;
    }
    
    public void ToMainMenu(){
        Time.timeScale=1f;
        SceneManager.LoadScene("Menu");
    }
}
