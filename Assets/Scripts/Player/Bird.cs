using System;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private Mover _mover;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private CollisionDetector _detector;
    [SerializeField] private Viewer _viewer;

    private Vector3 _startPosition = new Vector3(0, 0, 1);
    private Quaternion _startRotation = Quaternion.Euler(0, 0, 0);

    public event Action Died;

    private void Awake()
    {
        transform.position = _startPosition;
    }

    private void OnEnable()
    {
        _playerInput.Jumped += _mover.Jump;
        _playerInput.Shooted += _gun.Shoot;
        _detector.Detected += Die;
    }

    private void OnDisable()
    {
        _playerInput.Jumped -= _mover.Jump;
        _playerInput.Shooted -= _gun.Shoot;
        _detector.Detected -= Die;
    }

    public void Die()
    {
        Died?.Invoke();

        _mover.StopMovement();

        _viewer.SetDiedSprite();
    }

    public void Reset()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
    }
}