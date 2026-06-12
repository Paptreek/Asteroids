using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [SerializeField] Bullet _bullet;
    [SerializeField] ParticleSystem _explosionEffect;

    [SerializeField] private float _secondsBetweenShots = 2.0f; // need to play around with this. Should be different based on enemy ship size

    private float _cannonTimer = 1.0f; // value is for first bullet, then it turns to _secondsBetweenShots for the rest
    private float _moveDirectionX;
    private float _moveDirectionY;
    private float _directionChangeTimer = 1.0f;
    private Vector3 _spawnLocation;

    private void Start()
    {
        SetPositionAndDirection();
    }

    private void Update()
    {
        _directionChangeTimer -= Time.deltaTime;
        _cannonTimer -= Time.deltaTime;

        Move();
        FireBullet();

        if (Mathf.Abs(transform.position.x) > 18.10f)
        {
            Destroy(gameObject);
        }

        ScreenManager.WrapAroundScreen(transform, 20.0f, 13.75f); // need to adjust this for each ship size later
    }

    private void SetPositionAndDirection()
    {
        float spawnLocationX = Random.value < 0.5 ? -16.25f : 16.25f;
        float spawnLocationY = Random.value < 0.5 ? -9.0f : 9.0f;
        _spawnLocation = new(spawnLocationX, spawnLocationY);

        _moveDirectionY = Random.value < 0.5 ? -2.5f : 2.5f;
        
        if (spawnLocationX < 0)
        {
            _moveDirectionX = 2.5f;
        }
        else
        {
            _moveDirectionX = -2.5f;
        }
        
        transform.position = _spawnLocation;
    }

    private void Move()
    {
        transform.Translate(_moveDirectionX * Time.deltaTime, _moveDirectionY * Time.deltaTime, 0);

        if (_directionChangeTimer <= 0)
        {
            ChangeMoveDirection();
            _directionChangeTimer = Random.Range(1.0f, 2.0f);
        }
    }

    private void ChangeMoveDirection()
    {
        _moveDirectionY = -_moveDirectionY;
    }

    private void FireBullet()
    {
        if (_cannonTimer <= 0)
        {
            float firingDirection = Random.Range(0, 360f);

            Bullet bullet = Instantiate(_bullet, transform.position, transform.rotation);
            bullet.SetFiringShip(FiringShip.Enemy);
            bullet.SetFiringDirection(firingDirection);

            Debug.Log($"Enemy firing bullet!");
            
            _cannonTimer = _secondsBetweenShots;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("PlayerBullet") || collision.CompareTag("Asteroid"))
        {
            Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
