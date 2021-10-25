using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public abstract class Menu : MonoBehaviour
{
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioClip _errorSound;

    protected AudioSource AudioSource;
    protected int CurrentScene;

    protected virtual void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        AudioSource.playOnAwake = false;
        CurrentScene = SceneManager.GetActiveScene().buildIndex;
    }

    protected void PlayClickSound()
    {
        AudioSource.PlayOneShot(_clickSound);
    }

    protected void PlayErrorSound()
    {
        AudioSource.PlayOneShot(_errorSound);
    }
}