using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Bullet : MonoBehaviour 
{   
    [SerializeField] protected float _speed = 20f;
    [SerializeField] protected float _delay = 0.1f;
    [SerializeField] protected float _direction;

    protected Rigidbody2D _body;

    protected void Awake()
    {
        _body = GetComponent<Rigidbody2D>();       
    }

    protected abstract void OnCollisionEnter2D(Collision2D collision);


    protected abstract void OnTriggerEnter2D(Collider2D collision);
    

    protected void Update()
    {
        _body.velocity = new Vector2(_direction * _speed, _body.velocity.y);
    }
}