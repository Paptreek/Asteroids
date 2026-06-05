using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float _moveSpeed = 500;
    private float _turnSpeed = 250;
    private Rigidbody2D _rb;
    private InputAction _moveAction;
    private InputAction _turnAction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _moveAction = InputSystem.actions.FindAction("Move");
        _turnAction = InputSystem.actions.FindAction("Turn");
    }

    private void FixedUpdate()
    {
        Accelerate();
        Turn();
    }

    private void Accelerate()
    {
        float moveValue = _moveAction.ReadValue<float>();
        float moveSpeed = _moveSpeed * Time.fixedDeltaTime;
        float maxVelocity = 2.5f;

        if (_rb.linearVelocityY <= maxVelocity)
        {
            _rb.AddRelativeForceY(moveValue * moveSpeed);
        }
    }

    private void Turn()
    {
        Vector2 inputValue = _turnAction.ReadValue<Vector2>();
        float turnSpeed = _turnSpeed * Time.fixedDeltaTime;

        if (inputValue.x == -1)
        {
            transform.eulerAngles += new Vector3(0, 0, turnSpeed);
        }
        else if (inputValue.x == 1)
        {
            transform.eulerAngles += new Vector3(0, 0, -turnSpeed);
        }
    }
}
