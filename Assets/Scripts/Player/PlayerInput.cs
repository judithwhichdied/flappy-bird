using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private KeyCode _jumpKey = KeyCode.Space;
    private KeyCode _shootKey = KeyCode.Mouse0;

    public event Action Jumped;
    public event Action Shooted;

    private void Update()
    {
        if (Input.GetKeyDown(_jumpKey))
            Jumped?.Invoke();

        if (Input.GetKeyDown(_shootKey))
            Shooted?.Invoke();
    }
}