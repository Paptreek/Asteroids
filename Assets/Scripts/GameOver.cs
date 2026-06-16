using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _explosionEffect;

    private bool _exploded;

    private float _moveSpeedX;
    private float _moveSpeedY;
    private float _rotationSpeed;
    private float _initialTimer = 1.5f;

    private void Start()
    {
        _moveSpeedX = Random.Range(-5.0f, 5.0f);
        _moveSpeedY = Random.Range(-5.0f, 5.0f);
        _rotationSpeed = Random.Range(-250.0f, 250.0f);
    }

    private void Update()
    {
        _initialTimer -= Time.deltaTime;

        if (_initialTimer <= 0)
        {
            if (!_exploded)
            {
                // play an explosion sound
                GameObject explosion = Instantiate(_explosionEffect, transform.position, Quaternion.identity);
                explosion.transform.localScale = new Vector3(2, 2, 2);
                _exploded = true;
            }

            transform.Translate(_moveSpeedX * Time.deltaTime, _moveSpeedY * Time.deltaTime, 0, Space.World);
            transform.Rotate(new Vector3(0, 0, _rotationSpeed * Time.deltaTime));
            ScreenManager.WrapAroundScreen(transform, 18.5f, 14.75f);
        }
    }
}
