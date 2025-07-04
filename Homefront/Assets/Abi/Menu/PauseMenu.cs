using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void restart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void back()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
