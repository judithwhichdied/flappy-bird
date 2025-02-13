using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyBullet _bullet;
    [SerializeField] private AudioClip _hitSFX;
    [SerializeField] private AudioClip _spawnSFX;

    private AudioSource _source;

    private EnemyMover _mover;

    public event Action<Enemy> Died;

    private float _delay = 2f;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
        _source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        StartCoroutine(StartShooting());      
    }

    private void OnDisable()
    {
        StopCoroutine(StartShooting());
    }

    private void Shoot()
    {
        Instantiate(_bullet, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), Quaternion.identity);
        _source.PlayOneShot(_spawnSFX);
    }

    public void ReadyToMove()
    {
        _mover.SetReady();
    }

    public void Die()
    {
        StartCoroutine(DelayingDie());
        _source.PlayOneShot(_hitSFX);
    }

    private IEnumerator DelayingDie()
    {
        float time = 0;

        while(time < _delay)
        {
            time += Time.deltaTime;

            yield return null;
        }

        Died?.Invoke(this);
    }

    private IEnumerator StartShooting()
    {
        float coolDown = 2;

        while (enabled)
        {
            float time = 0;

            while (time < coolDown)
            {
                time += Time.deltaTime;

                yield return null;
            }

            Shoot();
        }
    }
}