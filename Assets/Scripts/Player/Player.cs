using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;

    private Rigidbody2D _rb;

    public int SmallShipsDestroyed { get; set; }
    public int LargeShipsDestroyed { get; set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ScreenManager.WrapAroundScreen(transform, 17.5f, 13.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid") || collision.CompareTag("EnemyShip") || collision.CompareTag("EnemyBullet"))
        {
            Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
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
}
