using UnityEngine;
using UnityEngine.Pool;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private EnemyBullet _bullet;

    private ObjectPool<EnemyBullet> _pool;

    private int _poolCapacity = 10;
    private int _poolMaxSize = 10;

    private void Awake()
    {
        _pool = new ObjectPool<EnemyBullet>
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

    private void OnGet(EnemyBullet bullet)
    {
        bullet.gameObject.SetActive(true);

        bullet.transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);

        bullet.transform.rotation = Quaternion.identity;

        bullet.Collided += _pool.Release;
    }

    private void OnRelease(EnemyBullet bullet)
    {
        bullet.gameObject.SetActive(false);

        bullet.Collided -= _pool.Release;
    }

    public void SpawnBullet()
    {
        _pool.Get();
    }
}