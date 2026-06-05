using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private InputAction _moveAction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _moveAction = InputSystem.actions.FindAction("Move");
    }

    private void FixedUpdate()
    {
        CheckForMovement();
    }

    private void CheckForMovement()
    {
        if (_moveAction.IsPressed())
        {
            _rb.linearVelocityY = 1; // need to find a way for movement to be gradually increasing/decreasing values. not just on/off
        }
        else
        {
            _rb.linearVelocityY = 0;
        }

    }
}
