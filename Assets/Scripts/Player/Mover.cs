using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(AudioSource), typeof(Bird))]
[RequireComponent(typeof(SpriteRenderer), typeof(Viewer))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private AudioClip _clip;

    private Rigidbody2D _body;
    private AudioSource _audioSource;
    private Viewer _viewer;

    private bool _isDied = false;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _viewer = GetComponent<Viewer>();
    }

    private void Update()
    {
        if (_isDied == false)
        {
            _body.velocity = new Vector2(_horizontalSpeed, _body.velocity.y);
        }      
    }

    public void Reset()
    {
        _isDied = false;
    }

    public void StopMovement()
    {
        _isDied = true;
    }

    public void Jump()
    {
        _body.velocity = new Vector2(_body.velocity.x, _jumpForce);

        StartCoroutine(SwitchSprite());

        _audioSource.PlayOneShot(_clip);
    }

    public float GetSpeed()
    {
        return _horizontalSpeed;
    }
   
    private IEnumerator SwitchSprite()
    {
        float wait = 0.1f;

        _viewer.SetJumpSprite();

        yield return new WaitForSeconds(wait);

        _viewer.SetDefaultSprite();
    }
}