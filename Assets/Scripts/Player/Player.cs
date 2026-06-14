using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private GameObject _respawnArea;

    private Rigidbody2D _rb;
    private float _respawnCheckTimer;

    public bool IsDead { get; set; }
    public int RemainingLives { get; private set; } = 100;
    public int SmallShipsDestroyed { get; set; }
    public int LargeShipsDestroyed { get; set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _respawnCheckTimer -= Time.deltaTime;

        ScreenManager.WrapAroundScreen(transform, 17.5f, 13.0f);

        CheckForRespawn();
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
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
        }
        else
        {
            Destroy(gameObject);
        }

        Debug.Log($"You died! Remaining Lives: {RemainingLives}");
    }

    private void CheckForRespawn()
    {
        Debug.Log($"Respawn Area Clear? {_respawnArea.GetComponent<RespawnArea>().IsClearOfDanger}");

        if (IsDead && _respawnCheckTimer <= 0)
        {
            if (_respawnArea.GetComponent<RespawnArea>().IsClearOfDanger)
            {
                ResetPosition();
                GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<PolygonCollider2D>().enabled = true;
                IsDead = false;
            }
        }
    }
}
