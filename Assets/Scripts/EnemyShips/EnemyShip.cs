using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private Player _player;
    [SerializeField] private Sprite _spriteLarge;
    [SerializeField] private Sprite _spriteSmall;
    [SerializeField] private PolygonCollider2D _colliderSmall;
    [SerializeField] private PolygonCollider2D _colliderLarge;

    private float _cannonTimer = 1.0f; // value is for first bullet, then it turns to _secondsBetweenShots for the rest
    private float _secondsBetweenShots;
    private float _moveDirectionX;
    private float _moveDirectionY;
    private float _moveSpeed;
    private float _directionChangeTimer = 1.0f;
    private Vector3 _spawnLocation;
    private SpriteRenderer _spriteRenderer;
    private PolygonCollider2D _collider;

    public ShipSize EnemyShipSize { get; private set; }
    public enum ShipSize { Large, Small }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<PolygonCollider2D>();
    }

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

    public void SetShipSize(ShipSize shipSize)
    {
        EnemyShipSize = shipSize;

        if (shipSize == ShipSize.Large)
        {
            _moveSpeed = 2.5f;
            _secondsBetweenShots = 2.0f;
            _spriteRenderer.sprite = _spriteLarge;
            _collider.points = _colliderLarge.points;
        }
        else
        {
            _moveSpeed = 5.0f;
            _secondsBetweenShots = 1.0f;
            _spriteRenderer.sprite = _spriteSmall;
            _collider.points = _colliderSmall.points;
            transform.localScale = Vector3.one;
        }
    }

    public void SetPlayerShip(Player playerShip)
    {
        _player = playerShip;
    }

    private void SetPositionAndDirection()
    {
        float spawnLocationX = Random.value < 0.5 ? -16.25f : 16.25f;
        float spawnLocationY = Random.Range(-9.0f, 9.0f);
        _spawnLocation = new(spawnLocationX, spawnLocationY);

        _moveDirectionY = Random.value < 0.5 ? -2.5f : 2.5f;
        
        if (spawnLocationX < 0)
        {
            _moveDirectionX = _moveSpeed;
        }
        else
        {
            _moveDirectionX = -_moveSpeed;
        }
        
        transform.position = _spawnLocation;
    }

    private void Move()
    {
        transform.Translate(_moveDirectionX * Time.deltaTime, _moveDirectionY * Time.deltaTime, 0);

        if (_directionChangeTimer <= 0)
        {
            _moveDirectionY = -_moveDirectionY;
            _directionChangeTimer = Random.Range(1.0f, 3.0f);
        }
    }

    private void FireBullet()
    {
        if (_cannonTimer <= 0)
        {
            float firingDirection;

            if (EnemyShipSize == ShipSize.Small)
            {
                firingDirection = AimTowardPlayer();
            }
            else
            {
                firingDirection = Random.Range(0, 360f);
            }

            Bullet bullet = Instantiate(_bullet, transform.position, transform.rotation);
            bullet.SetFiringShip(FiringShip.Enemy);
            bullet.SetFiringDirection(firingDirection);

            //Debug.Log($"Enemy firing bullet! Enemy Pos: {position}, Player Pos: {playerPosition}");
            
            _cannonTimer = _secondsBetweenShots;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            Debug.Log($"Enemy ship collided with an asteroid!");

            Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Player") || collision.CompareTag("PlayerBullet"))
        {
            Debug.Log($"Enemy ship destroyed by player!");

            if (EnemyShipSize == ShipSize.Large)
            {
                _player.LargeShipsDestroyed++;
                Debug.Log($"Large enemy ship destroyed by player! Total: {_player.LargeShipsDestroyed}");
            }
            else
            {
                _player.SmallShipsDestroyed++;
                Debug.Log($"Small enemy ship destroyed by player! Total: {_player.SmallShipsDestroyed}");
            }

            Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private float AimTowardPlayer()
    {
        float firingDirection;
        Vector3 position = transform.position;
        Vector3 playerPosition = Vector3.zero;

        if (_player != null)
        {
            playerPosition = _player.transform.position;

            if (position.x - playerPosition.x < 0 && position.y - playerPosition.y < 0)
            {
                firingDirection = Random.Range(270f, 360f);
            }
            else if (position.x - playerPosition.x < 0 && position.y - playerPosition.y > 0)
            {
                firingDirection = Random.Range(180f, 270f);
            }
            else if (position.x - playerPosition.x > 0 && position.y - playerPosition.y > 0)
            {
                firingDirection = Random.Range(90f, 180f);
            }
            else if (position.x - playerPosition.x > 0 && position.y - playerPosition.y < 0)
            {
                firingDirection = Random.Range(0f, 90f);
            }
            else
            {
                firingDirection = Random.Range(0, 360f);
            }
        }
        else
        {
            firingDirection = Random.Range(0, 360f);
        }

        return firingDirection;
    }
}
