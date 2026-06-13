using UnityEngine;

public class EnemyShipCollision : MonoBehaviour
{
    [SerializeField] private GameObject _ship;
    [SerializeField] private ParticleSystem _explosionEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            Debug.Log($"Enemy ship collided with an asteroid!");

            Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            Destroy(_ship);
        }
        else if (collision.CompareTag("Player") || collision.CompareTag("PlayerBullet"))
        {
            Debug.Log($"Enemy ship destroyed by player!");

            Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            Destroy(_ship);
        }
    }
}
