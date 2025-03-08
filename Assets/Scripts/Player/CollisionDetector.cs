using System;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public event Action Detected;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<VerticalWall>(out _))
        {
            Detected?.Invoke();
        }
    }
}
