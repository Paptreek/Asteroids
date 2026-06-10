using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;

    private void Update()
    {
        ScreenManager.WrapAroundScreen(transform, 17.5f, 13.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
