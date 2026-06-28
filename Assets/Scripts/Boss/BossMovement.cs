using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private float _moveSpeedX = 2.5f;
    private float _moveSpeedY = 2.5f;
    private float _bonusSpeed;
    private float _movementChangeTimer = 5.0f;

    private void Start()
    {
        _moveSpeedX = Random.value < 0.5f ? Random.Range(-3.0f, -2.0f) : Random.Range(2.0f, 3.0f);
        _moveSpeedY = Random.value < 0.5f ? Random.Range(-3.0f, -2.0f) : Random.Range(2.0f, 3.0f);
    }

    private void Update()
    {
        _movementChangeTimer -= Time.deltaTime;

        Move();
    }

    public void IncreaseSpeed()
    {
        _bonusSpeed = 2.5f;
    }

    private void Move()
    {
        float moveSpeedX = _moveSpeedX < 0 ? _moveSpeedX - _bonusSpeed : _moveSpeedX + _bonusSpeed;
        float moveSpeedY = _moveSpeedY < 0 ? _moveSpeedY - _bonusSpeed : _moveSpeedY + _bonusSpeed;

        transform.Translate(moveSpeedX * Time.deltaTime, moveSpeedY * Time.deltaTime, 0);

        ChangeDirectionNearWalls();

        if (_movementChangeTimer <= 0)
        {
            ChangeSpeedAndDirection();
        }
    }

    private void ChangeSpeedAndDirection()
    {
        _moveSpeedX = Random.value < 0.5f ? Random.Range(-3.0f, -2.0f) : Random.Range(2.0f, 3.0f);
        _moveSpeedY = Random.value < 0.5f ? Random.Range(-3.0f, -2.0f) : Random.Range(2.0f, 3.0f);

        _movementChangeTimer = 5.0f;
    }

    private void ChangeDirectionNearWalls()
    {
        if (Mathf.Abs(transform.localPosition.x) >= 14)
        {
            _moveSpeedX = -_moveSpeedX;
        }

        if (Mathf.Abs(transform.localPosition.y) >= 9.5f)
        {
            _moveSpeedY = -_moveSpeedY;
        }
    }
}
