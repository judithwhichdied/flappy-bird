using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private AudioClip _clip;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void ShootBullet()
    {
        Bullet bullet;

        bullet = Instantiate(_bullet);

        bullet.transform.position = transform.position;

        _audioSource.PlayOneShot(_clip);
    }
}
