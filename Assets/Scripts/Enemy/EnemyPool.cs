using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class EnemyPool : MonoBehaviour
{  
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Bird _target;

    private float _minPositionY = -5f;
    private float _maxPositionY = 5f;

    private int _maxEnemyCount = 3;
    private int _spawnDistance = 15;

    private int _poolCapacity = 3;
    private int _poolMaxSize = 3;

    private float _delay = 2f;

    private ObjectPool<Enemy> _pool;

    public event Action EnemyDied;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>
            (
                 createFunc: () => Instantiate(_enemy),
                 actionOnGet: OnGet,
                 actionOnRelease: (enemy) => ReleaseEnemy(enemy),
                 actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
                 collectionCheck: true,
                 defaultCapacity: _poolCapacity,
                 maxSize: _poolMaxSize
            );
    }

    private void Start()
    {
        for (int i = 0; i < _maxEnemyCount; i++)
            Spawn();
    }

    public void ClearPool()
    {
        _pool.Clear();
    }

    private void OnGet(Enemy enemy)
    {       
        enemy.transform.position = GetPosition();

        enemy.gameObject.SetActive(true);

        StartCoroutine(Waiting(enemy));

        enemy.Died += _pool.Release;
        enemy.Died += ChangeScore;
    }

    private void ReleaseEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);

        enemy.Died -= _pool.Release;
        enemy.Died -= ChangeScore;

        StartCoroutine(DelaySpawning());
    }

    private IEnumerator DelaySpawning()
    {
        yield return new WaitForSeconds(_delay);

        Spawn();
    }

    private void ChangeScore(Enemy _)
    {
        EnemyDied?.Invoke();
    }

    private void Spawn()
    {
        _pool.Get();
    }

    private Vector3 GetPosition()
    {
        return new Vector3(_target.transform.position.x + _spawnDistance, Random.Range(_minPositionY, _maxPositionY), _enemy.transform.position.z);
    }

    private IEnumerator Waiting(Enemy enemy)
    {
        float distance = 8;

        while (enemy.transform.position.x > (_target.transform.position.x + distance))
        {
            yield return null;
        }

        enemy.ReadyToMove();
    }
}