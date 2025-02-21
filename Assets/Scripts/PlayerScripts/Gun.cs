using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private AudioClip _clip;

    private AudioSource _audioSource;

    private bool _isShooted = false;
    private int _cooldown = 1;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        if (_isShooted == false && Time.timeScale > 0)
        {
            SpawnBullet();

            StartCoroutine(StartCooldown());
        }
    }

    private void SpawnBullet()
    {
        Bullet bullet;

        bullet = Instantiate(_bullet);

        bullet.transform.position = transform.position;

        _audioSource.PlayOneShot(_clip);
    }

    private IEnumerator StartCooldown()
    {
        _isShooted = true;

        yield return new WaitForSeconds(_cooldown);

        _isShooted = false;
    }
}