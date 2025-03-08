using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    [SerializeField] private PlayerBullet _bullet;
    [SerializeField] private AudioClip _clip;

    private AudioSource _audioSource;

    private bool _isShooted = false;
    private int _cooldown = 1;
    private int _poolCapacity = 3;
    private int _poolMaxSize = 3;

    private ObjectPool<PlayerBullet> _pool;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        _pool = new ObjectPool<PlayerBullet>
            (
                 createFunc: () => Instantiate(_bullet),
                 actionOnGet: OnGet,
                 actionOnRelease: (bullet) => OnRelease(bullet),
                 actionOnDestroy: (bullet) => Destroy(bullet.gameObject),
                 collectionCheck: true,
                 defaultCapacity: _poolCapacity,
                 maxSize: _poolMaxSize
            );
    }

    public void Shoot()
    {
        if (_isShooted == false && Time.timeScale > 0)
        {
            SpawnBullet();

            StartCoroutine(StartCooldown());
        }
    }

    private void OnGet(PlayerBullet bullet)
    {
        bullet.transform.position = transform.position;

        bullet.gameObject.SetActive(true);

        bullet.Collided += _pool.Release;
    }

    private void OnRelease(PlayerBullet bullet)
    {
        bullet.gameObject.SetActive(false);

        bullet.Collided -= _pool.Release;
    }

    private void SpawnBullet()
    {
        _pool.Get();

        _audioSource.PlayOneShot(_clip);
    }

    private IEnumerator StartCooldown()
    {
        _isShooted = true;

        yield return new WaitForSeconds(_cooldown);

        _isShooted = false;
    }
}