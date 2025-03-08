using UnityEngine;

public class MapMover : MonoBehaviour
{
    private float _positionXScale = 12.9f;

    private Vector3 _startPosition = new Vector3(3, 0.5f, 1);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Bird>(out Bird _))
        {
            transform.position = new Vector3(transform.position.x + _positionXScale, transform.position.y, transform.position.z);
        }
    }

    public void Reset()
    {
        transform.position = _startPosition;
    }
}
