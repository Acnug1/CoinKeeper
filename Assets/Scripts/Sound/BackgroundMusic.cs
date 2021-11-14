using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private bool _isLooping;
    [SerializeField] [Range(0, 1)] private float _volume;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = _isLooping;
        _audioSource.volume = _volume;
        _audioSource.clip = _backgroundMusic;
    }

    private void OnEnable()
    {
        _audioSource.Play();
    }

    private void OnDisable()
    {
        _audioSource.Stop();
    }
}