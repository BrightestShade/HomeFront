using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMEnu : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Play Again"); 
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
