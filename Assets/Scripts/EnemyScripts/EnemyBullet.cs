using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(AudioSource))]
public class EnemyBullet : Bullet
{
    [SerializeField] private AudioClip _clip;
    
    private AudioSource _audioSource;

    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Bird bird))
        {
            bird.Die();
            _audioSource.PlayOneShot(_clip);
        }
    }
}