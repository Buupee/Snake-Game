using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnPlayHangler(){
        SceneManager.LoadScene(1);
    }
    public void OnExitHangler(){
        Application.Quit();
    }
    public GameObject Settings_Menu;
    public void Settings_On(){
        Settings_Menu.SetActive(true);
    }
    public void Settings_Off(){
        Settings_Menu.SetActive(false);
    }
}
