using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private GameObject _respawnArea;
    [SerializeField] private AbilityManager _abilityManager;

    private Rigidbody2D _rb;
    private PolygonCollider2D _collider;
    private SpriteRenderer _spriteRenderer;
    private float _respawnCheckTimer;

    public bool IsDead { get; set; }
    public int RemainingLives { get; private set; } = 3;
    public int SmallShipsDestroyed { get; set; }
    public int LargeShipsDestroyed { get; set; }
    public float EnableCollisionTimer { get; private set; } = 1.5f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<PolygonCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!_collider.enabled && !IsDead && !_abilityManager.ShieldActivated)
        {
            //Debug.Log($"COLLIDER DISABLED!");
            EnableCollisionTimer -= Time.deltaTime;
        }

        _respawnCheckTimer -= Time.deltaTime;

        ScreenManager.WrapAroundScreen(transform, 17.5f, 13.0f);

        CheckForRespawnIfDead();
        EnableColliderAfterWait();
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
            _respawnCheckTimer = 0.25f;
            RemainingLives--;
            IsDead = true;

            _spriteRenderer.enabled = false;
            _collider.enabled = false;
        }
        else
        {
            Destroy(gameObject);
        }

        Debug.Log($"You died! Remaining Lives: {RemainingLives}");
    }

    private void CheckForRespawnIfDead()
    {
        //Debug.Log($"Respawn Area Clear? {_respawnArea.GetComponent<RespawnArea>().IsClearOfDanger}");

        if (IsDead && _respawnCheckTimer <= 0)
        {
            if (_respawnArea.GetComponent<RespawnArea>().IsClearOfDanger)
            {
                ResetPosition();

                IsDead = false;

                _spriteRenderer.enabled = true;
                EnableCollisionTimer = 1.5f;
            }
        }
    }

    private void EnableColliderAfterWait()
    {
        if (!_collider.enabled && EnableCollisionTimer <= 0)
        {
            _collider.enabled = true;
            EnableCollisionTimer = 1.5f;
        }
    }
}
