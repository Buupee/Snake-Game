using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void RestartGame()
    {
        // Загружаем текущую сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}