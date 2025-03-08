using System;
using UnityEngine;

public class PlayerBullet : Bullet
{
    public event Action<PlayerBullet> Collided;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            Collided?.Invoke(this);
            enemy.Die();
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
