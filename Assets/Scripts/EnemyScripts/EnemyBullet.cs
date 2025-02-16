using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(AudioSource))]
public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    
    private AudioSource _audioSource;

    private Rigidbody2D _body;

    private float _speed = 3f;

    private float _delay = 0.1f;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _body.velocity = new Vector2(-_speed, _body.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Bird> (out Bird bird))
        {
            bird.Die();
            _audioSource.PlayOneShot(_clip);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<HorizontalWall>(out HorizontalWall _))
            Destroy(gameObject, _delay);
    }
}
