using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : Menu
{
    public void Restart()
    {
        PlayClickSound();
        Time.timeScale = 1;
        SceneManager.LoadScene(CurrentScene);
    }

    public void ReturnToMainMenu()
    {
        PlayClickSound();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
