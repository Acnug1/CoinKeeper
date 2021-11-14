using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Menu
{
    [SerializeField] private GameObject[] _disablePanels;

    private int _newScene;

    protected override void Start()
    {
        base.Start();

        foreach (var disablePanel in _disablePanels)
        {
            disablePanel.SetActive(false);
        }
    }

    public void LoadGame()
    {
        if (CurrentScene + 1 < SceneManager.sceneCountInBuildSettings)
        {
            PlayClickSound();
            CurrentScene++;
            _newScene = CurrentScene;
            Time.timeScale = 1;
            SceneManager.LoadScene(_newScene);
        }
        else
            PlayErrorSound();
    }

    public void OpenPanel(GameObject panel)
    {
        PlayClickSound();

        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        PlayClickSound();

        panel.SetActive(false);
    }

    public void Exit()
    {
        PlayClickSound();
        Application.Quit();
    }
}
