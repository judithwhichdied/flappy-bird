using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(AudioSource), typeof(Bird))]
[RequireComponent(typeof(SpriteRenderer))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _jumpedSprite;
    [SerializeField] private Sprite _diedSprite;
    [SerializeField] private AudioClip _clip;

    private Rigidbody2D _body;
    private Bird _bird;
    private AudioSource _audioSource;
    private SpriteRenderer _renderer;

    private bool _isDied = false;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _bird = GetComponent<Bird>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_isDied == false)
        {
            _body.velocity = new Vector2(_horizontalSpeed, _body.velocity.y);
        }      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<VerticalWall>(out _))
            _bird.Die();
    }

    private void OnEnable()
    {
        _bird.Died += StopMovement;
    }

    private void OnDisable()
    {
        _bird.Died -= StopMovement;
    }

    public void Reset()
    {
        _isDied = false;
    }

    public void Jump()
    {
        _body.velocity = new Vector2(_body.velocity.x, _jumpForce);

        StartCoroutine(SwitchSprite());

        _audioSource.PlayOneShot(_clip);
    }

    private void StopMovement()
    {
        _isDied = true;

        _renderer.sprite = _diedSprite;
    }

    public float GetSpeed()
    {
        return _horizontalSpeed;
    }
   
    private IEnumerator SwitchSprite()
    {
        float time = 0;
        float wait = 0.1f;

        _renderer.sprite = _jumpedSprite;

        while (time < wait)
        {
            time += Time.deltaTime;

            yield return null;
        }

        _renderer.sprite = _defaultSprite;
    }
}