using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private float _moveSpeedX = 2.5f;
    private float _moveSpeedY = 2.5f;
    private float _movementChangeTimer = 5.0f;

    private void Start()
    {
        _moveSpeedX = Random.value < 0.5f ? Random.Range(-3.0f, -2.0f) : Random.Range(2.0f, 3.0f);
        _moveSpeedY = Random.value < 0.5f ? Random.Range(-3.0f, -2.0f) : Random.Range(2.0f, 3.0f);
    }

    private void Update()
    {
        _movementChangeTimer -= Time.deltaTime;

        if (_movementChangeTimer <= 0)
        {
            ChangeMovement();
        }

        transform.Translate(_moveSpeedX * Time.deltaTime, _moveSpeedY * Time.deltaTime, 0);

        if (Mathf.Abs(transform.localPosition.x) >= 14)
        {
            _moveSpeedX = -_moveSpeedX;
        }

        if (Mathf.Abs(transform.localPosition.y) >= 9.5f)
        {
            _moveSpeedY = -_moveSpeedY;
        }
    }

    private void ChangeMovement()
    {
        _moveSpeedX = Random.value < 0.5f ? Random.Range(-3.0f, -2.0f) : Random.Range(2.0f, 3.0f);
        _moveSpeedY = Random.value < 0.5f ? Random.Range(-3.0f, -2.0f) : Random.Range(2.0f, 3.0f);

        _movementChangeTimer = 5.0f;
    }
}
