using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites = new Sprite[3];
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private AudioClip _explosionSoundLow;
    [SerializeField] private AudioClip _explosionSoundMid;
    [SerializeField] private AudioClip _explosionSoundHigh;
    
    private float _moveSpeed;
    private float _rotationSpeed;
    private Vector3 _moveDirection;
    private Vector3 _spawnLocation;
    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _collider;
    private AsteroidManager _asteroidManager;
    private PowerUpManager _powerUpManager;
    private AudioClip _explosionSound;

    public bool HasBeenHit { get; private set; }
    public bool DestroyedByPlayer { get; private set; }
    public Size AsteroidSize { get; private set; }
    public enum Size { Large, Medium, Small }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        transform.position = _spawnLocation;
        _rotationSpeed = Random.Range(-100.0f, 100.0f);

        AssignStartingRotation();
    }

    private void Update()
    {
        Move();
        Rotate();
        WrapAroundScreen();
    }

    public void SetAsteroidManager(AsteroidManager asteroidManager)
    {
        _asteroidManager = asteroidManager;
    }

    public void SetInitialSpawnData(Vector3 direction, Vector3 location)
    {
        _moveDirection = direction;
        _spawnLocation = location;
    }

    public void SetSize(Size size)
    {
        if (size == Size.Large)
        {
            AsteroidSize = Size.Large;
            _moveSpeed = 3;
            _spriteRenderer.sprite = _sprites[0];
            _collider.radius = 1.0f;
            _explosionSound = _explosionSoundLow;
        }

        if (size == Size.Medium)
        {
            AsteroidSize = Size.Medium;
            _moveSpeed = 4;
            _spriteRenderer.sprite = _sprites[1];
            _collider.radius = 0.65f;
            _explosionSound = _explosionSoundMid;
        }

        if (size == Size.Small)
        {
            AsteroidSize = Size.Small;
            _moveSpeed = 5;
            _spriteRenderer.sprite = _sprites[2];
            _collider.radius = 0.375f;
            _explosionSound = _explosionSoundHigh;
        }
    }

    public void SetPowerUpManager(PowerUpManager powerUpManager)
    {
        _powerUpManager = powerUpManager;
    }

    private void AssignStartingRotation()
    {
        float randomRotation = Random.Range(0.0f, 360.0f);
        transform.eulerAngles = new Vector3(0, 0, randomRotation);
    }

    private void Move()
    {
        float moveSpeedX = _moveDirection.x * _moveSpeed * Time.deltaTime;
        float moveSpeedY = _moveDirection.y * _moveSpeed * Time.deltaTime;

        transform.Translate(moveSpeedX, moveSpeedY, 0, Space.World);
    }

    private void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, _rotationSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet") || collision.CompareTag("EnemyShip") || collision.CompareTag("GameOver"))
        {
            AudioManager.Instance.PlayEnemyDamaged(_explosionSound);
    
            if (_asteroidManager.Asteroids.Count > 1)
            {
                Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            }

            HasBeenHit = true;
        }

        if (collision.CompareTag("PlayerBullet") || collision.CompareTag("Player"))
        {
            AudioManager.Instance.PlayEnemyDamaged(_explosionSound);

            if (_asteroidManager.Asteroids.Count > 1)
            {
                Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            }

            HasBeenHit = true;
            DestroyedByPlayer = true;
            _powerUpManager.MaybeDropPowerUp(transform.position, 10);
        }
    }

    private void WrapAroundScreen()
    {
        if (AsteroidSize == Size.Large)
        {
            ScreenManager.WrapAroundScreen(transform, 19.5f, 15.25f);
        }
        else if (AsteroidSize == Size.Medium)
        {
            ScreenManager.WrapAroundScreen(transform, 18.5f, 14.25f);
        }
        else if (AsteroidSize == Size.Small)
        {
            ScreenManager.WrapAroundScreen(transform, 18.1f, 13.75f);
        }
    }
}
