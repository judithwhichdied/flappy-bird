using System;
using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private PlayerInput _input;

    private bool _isShooted = false;

    private int _cooldown = 1;

    private Vector3 _startPosition = new Vector3(0, 0, 1);
    private Quaternion _startRotation = Quaternion.Euler(0, 0, 0);

    public event Action Died;

    private void Awake()
    {
        transform.position = _startPosition;
    }

    private void OnEnable()
    {
        _input.Shooted += Shoot;
    }

    private void OnDisable()
    {
        _input.Shooted -= Shoot;
    }

    public void Die()
    {
        Died?.Invoke();
    }

    public void Reset()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        _isShooted = false;
    }

    private void Shoot()
    {
        if (_isShooted == false && Time.timeScale > 0)
        {
            _gun.ShootBullet();

            StartCoroutine(StartCooldown());
        }
    }

    private IEnumerator StartCooldown()
    {
        float currentTime = 0;

        _isShooted = true;

        while(currentTime < _cooldown)
        {
            currentTime += Time.deltaTime;

            yield return null;
        }

        _isShooted = false;
    }
}