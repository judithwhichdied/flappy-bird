using System;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private Mover _mover;
    [SerializeField] private PlayerInput _playerInput;

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
    }

    private void OnDisable()
    {
        _playerInput.Jumped -= _mover.Jump;
        _playerInput.Shooted -= _gun.Shoot;
    }

    public void Die()
    {
        Died?.Invoke();
    }

    public void Reset()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
    }
}