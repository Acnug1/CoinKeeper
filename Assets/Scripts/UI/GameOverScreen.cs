using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Player _player;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
    }

    private void OnEnable()
    {
        _player.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _player.GameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        StartCoroutine(CanvasGroupAlphaIncrease());
    }

    private IEnumerator CanvasGroupAlphaIncrease()
    {
        var waitForSeconds = new WaitForSeconds(0.1f);

        for (int i = 0; i <= 10; i++)
        {
            _canvasGroup.alpha = i / 10f;
            yield return waitForSeconds;
        }

        Time.timeScale = 0;
    }
}
