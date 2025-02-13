using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bird : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private AudioClip _clip;

    private AudioSource _source;

    private KeyCode _shootKey = KeyCode.Mouse0;

    private bool _isShooted = false;

    private int _cooldown = 1;

    private Vector3 _startPosition = new Vector3(0, 0, 1);
    private Quaternion _startRotation = Quaternion.Euler(0, 0, 0);

    public event Action Died;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();

        transform.position = _startPosition;
    }

    private void Update()
    {
        if(Time.timeScale == 1)
        {
            if (Input.GetKeyDown(_shootKey) && _isShooted == false)
            {
                _gun.ShootBullet();

                StartCoroutine(StartCooldown());
            }
        }          
    }

    public void Die()
    {
        Died?.Invoke();
        _source.PlayOneShot(_clip);
    }

    public void Reset()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        _isShooted = false;
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