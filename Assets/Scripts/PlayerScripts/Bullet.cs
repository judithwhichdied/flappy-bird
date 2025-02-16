using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D _body;

    private float _speed = 20f;

    private float _delay = 0.1f;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<HorizontalWall>(out HorizontalWall _))
            Destroy(gameObject, _delay);
    }

    private void Update()
    {
        _body.velocity = new Vector2(_speed, _body.velocity.y);
    }
}
