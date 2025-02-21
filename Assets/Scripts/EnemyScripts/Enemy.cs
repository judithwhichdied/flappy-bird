using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(AudioSource), typeof(EnemyAttacker))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private AudioClip _hitSFX;
    [SerializeField] private AudioClip _spawnSFX;

    private EnemyAttacker _attacker;
    private AudioSource _source;
    private EnemyMover _mover;

    private float _delay = 2f;

    public event Action<Enemy> Died;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
        _source = GetComponent<AudioSource>();
        _attacker = GetComponent<EnemyAttacker>();
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
        _attacker.SpawnBullet();
        _source.PlayOneShot(_spawnSFX);
    }

    public void ReadyToMove()
    {
        _mover.SetReady();
    }

    public void Die()
    {
        StartCoroutine(DieDelaying());
        _source.PlayOneShot(_hitSFX);
    }

    private IEnumerator DieDelaying()
    {
        yield return new WaitForSeconds(_delay);

        Died?.Invoke(this);
    }

    private IEnumerator StartShooting()
    {
        float coolDown = 2;

        while (enabled)
        {
            yield return new WaitForSeconds(coolDown);

            Shoot();
        }
    }
}