using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private GameObject _respawnArea;
    [SerializeField] private PowerUpManager _powerUpManager;

    private Rigidbody2D _rb;
    private PolygonCollider2D _collider;
    private SpriteRenderer _spriteRenderer;
    private float _respawnCheckTimer;
    private float _colliderDisabledTimer;
    private float _spriteOnTimer;
    private float _spriteOffTimer = 0.25f;
    private string[] _enemyColliderTags = { "Asteroid", "EnemyShip", "EnemyBullet", "EnemyCannon", "BossCore" };

    public bool IsDead { get; set; }
    public bool SpriteBlinkingActive { get; private set; }
    public int DeathCount { get; private set; }
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

        if (SpriteBlinkingActive)
        {
            HandleSpriteBlinking();
        }
        else if (!SpriteBlinkingActive && !IsDead)
        {
            _spriteRenderer.enabled = true;
        }

        ScreenManager.WrapAroundScreen(transform, 17.5f, 13.0f);

        RespawnIfSafe();
        ManageCollider();
    }

    public void ResetPosition(Vector3 position)
    {
        if (this != null)
        {
            transform.position = position;
            transform.eulerAngles = Vector3.zero;
            _rb.linearVelocity = Vector2.zero;
        }
    }

    public void ActivateIFrames(float timeInSeconds)
    {
        _colliderDisabledTimer = timeInSeconds;
        SpriteBlinkingActive = true;
    }

    public bool IsAliveAndReady()
    {
        if (!IsDead && !SpriteBlinkingActive)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (string tag in _enemyColliderTags)
        {
            if (collision.CompareTag(tag))
            {
                Die();
                Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            }
        }
    }

    private void Die()
    {
        _spriteRenderer.enabled = false;
        _collider.enabled = false;

        _colliderDisabledTimer = 100.0f; // this is a janky solution for keeping the player safe until respawn happens

        _respawnCheckTimer = 0.25f;
        DeathCount++;
        IsDead = true;

        Debug.Log($"You died! Death Count: {DeathCount}");
    }

    private void RespawnIfSafe()
    {
        if (IsDead && _respawnCheckTimer <= 0)
        {
            if (_respawnArea.GetComponent<RespawnArea>().IsClearOfDanger)
            {
                ResetPosition(Vector3.zero);

                IsDead = false;

                _spriteRenderer.enabled = true;

                _colliderDisabledTimer = 2.0f;

                SpriteBlinkingActive = true;
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
            SpriteBlinkingActive = false;
        }
    }

    private void HandleSpriteBlinking()
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
