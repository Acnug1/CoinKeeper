using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Vector3 _rotateAxis;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private ParticleSystem _coinVFX;
    [SerializeField] private AudioClip _soundEffect;

    private void Update()
    {
        transform.Rotate(_rotateAxis.normalized * _rotateSpeed * Time.deltaTime);
    }

    public void PlayVFX()
    {
        if (_coinVFX)
        {
            ParticleSystem coinVFX = Instantiate(_coinVFX, transform.position, Quaternion.identity);

            if (coinVFX.TryGetComponent(out AudioSource audioSource) && _soundEffect)
                audioSource.PlayOneShot(_soundEffect);
        }
    }
}
