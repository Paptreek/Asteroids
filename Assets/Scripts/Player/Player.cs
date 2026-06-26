using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private GameObject _respawnArea;
    [SerializeField] private PowerUpManager _powerUpManager;

    private Rigidbody2D _rb;
    private PolygonCollider2D _collider;
    private SpriteRenderer _spriteRenderer;
    private bool _spriteBlinkingActive;
    private float _respawnCheckTimer;
    private float _colliderDisabledTimer;
    private float _spriteOnTimer;
    private float _spriteOffTimer = 0.25f;

    public bool IsDead { get; set; }
    public int RemainingLives { get; private set; } = 3;
    public int SmallShipsDestroyed { get; set; }
    public int LargeShipsDestroyed { get; set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<PolygonCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _colliderDisabledTimer -= Time.deltaTime;
        _respawnCheckTimer -= Time.deltaTime;

        _spriteOffTimer -= Time.deltaTime;
        _spriteOnTimer -= Time.deltaTime;

        if (_spriteBlinkingActive)
        {
            ActivateSpriteBlinking();
        }

        ScreenManager.WrapAroundScreen(transform, 17.5f, 13.0f);

        RespawnIfSafe();
        ManageCollider();
    }

    public void ResetPosition()
    {
        if (this != null)
        {
            transform.position = Vector3.zero;
            transform.eulerAngles = Vector3.zero;
            _rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid") || collision.CompareTag("EnemyShip") || collision.CompareTag("EnemyBullet"))
        {
            Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            Die();
        }
    }

    private void Die()
    {
        if (RemainingLives > 0)
        {
            _spriteRenderer.enabled = false;
            _collider.enabled = false;

            _colliderDisabledTimer = 100.0f; // this is a janky solution for keeping the player safe until respawn happens

            _respawnCheckTimer = 0.25f;
            RemainingLives--;
            IsDead = true;
        }
        else
        {
            Destroy(gameObject);
        }

        Debug.Log($"You died! Remaining Lives: {RemainingLives}");
    }

    private void RespawnIfSafe()
    {
        if (IsDead && _respawnCheckTimer <= 0)
        {
            if (_respawnArea.GetComponent<RespawnArea>().IsClearOfDanger)
            {
                ResetPosition();

                IsDead = false;

                _spriteRenderer.enabled = true;

                _colliderDisabledTimer = 2.0f;

                _spriteBlinkingActive = true;
            }
        }
    }

    private void ManageCollider()
    {
        if (_powerUpManager.ShieldActivated || _colliderDisabledTimer > 0)
        {
            _collider.enabled = false;
        }
        else
        {
            _collider.enabled = true;
            _spriteBlinkingActive = false;
        }
    }

    private void ActivateSpriteBlinking()
    {
        if (_spriteRenderer.enabled)
        {
            _spriteOnTimer -= Time.deltaTime;

            if (_spriteOnTimer <= 0)
            {
                _spriteRenderer.enabled = false;
                _spriteOffTimer = 0.25f;
            }
        }
        else
        {
            _spriteOffTimer -= Time.deltaTime;

            if (_spriteOffTimer <= 0)
            {
                _spriteRenderer.enabled = true;
                _spriteOnTimer = 0.25f;
            }
        }
    }
}
