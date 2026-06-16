using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float _moveSpeed = 750f;
    private float _turnSpeed = 250;
    private Rigidbody2D _rb;
    private InputAction _moveAction;
    private InputAction _turnAction;
    private Animator _animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _moveAction = InputSystem.actions.FindAction("Move");
        _turnAction = InputSystem.actions.FindAction("Turn");
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Accelerate();
        Turn();
    }

    public void IncreaseMoveSpeed(float amountToIncrease)
    {
        _moveSpeed += amountToIncrease;
    }

    public void IncreaseTurnSpeed(float amountToIncrease)
    {
        _turnSpeed += amountToIncrease;
    }

    private void Accelerate()
    {
        float moveSpeed = _moveSpeed * Time.fixedDeltaTime;

        if (_moveAction.IsPressed())
        {
            _animator.SetTrigger("Moving");
            _rb.AddRelativeForceY(moveSpeed);
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
