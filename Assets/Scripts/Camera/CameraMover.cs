using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _bird;

    private float _speed = 5f;

    private void LateUpdate()
    {
        transform.position = Vector2.Lerp(new Vector2(transform.position.x, 0), new Vector2(_bird.transform.position.x, 0) , _speed);
    }
}
