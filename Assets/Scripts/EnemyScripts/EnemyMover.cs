using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    private const string LayerName = "Enemy";

    [SerializeField] private Mover _bird;
    [SerializeField] private EnemyPool _spawner;

    private Rigidbody2D _body;
    private float _verticalSpeed = 3f;
    private bool _isReady = false;
    private float _direction = 1;
    private int _colliderObject;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();

        _colliderObject = LayerMask.NameToLayer(LayerName);

        Physics2D.IgnoreLayerCollision(_colliderObject, _colliderObject, true);
    }

    private void OnEnable()
    {
        _isReady = false;
        SetRandomDirection();

        StartCoroutine(StartMoving());
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<VerticalWall>(out VerticalWall _))
            SwitchDirection();
    }

    public void SetReady()
    {
        _isReady = true;
    }

    private void SwitchDirection()
    {
        _direction *= -1;
    }

    private void SetRandomDirection()
    {
        int minValue = 0;
        int maxValue = 100;

        int neededValue =50;

        int successValue = Random.Range(minValue, maxValue);

        if (successValue < neededValue)
            SwitchDirection();
    }

    private IEnumerator StartMoving()
    {
        while(_isReady == false)
        {
            yield return null;
        }

        while(_isReady && enabled)
        {
            _body.velocity = new Vector2(_bird.GetSpeed(), _direction * _verticalSpeed);

            yield return null;
        }
    }   
}