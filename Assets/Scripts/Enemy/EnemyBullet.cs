using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(AudioSource))]
public class EnemyBullet : Bullet
{
    [SerializeField] private AudioClip _clip;
    
    private AudioSource _audioSource;

    public event Action<EnemyBullet> Collided;

    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Bird bird))
        {           
            _audioSource.PlayOneShot(_clip);

            Collided?.Invoke(this);

            bird.Die();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<HorizontalWall>(out _))
        {
            Collided?.Invoke(this);
        }
    }
}