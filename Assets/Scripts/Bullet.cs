using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _destroyTimer = 0.75f;
    private float _baseSpeed = 25;

    [field: SerializeField] public float FiringShipSpeed { get; set; }

    private void Update()
    {
        _destroyTimer -= Time.deltaTime;

        transform.Translate(new Vector2(0, (FiringShipSpeed + _baseSpeed) * Time.deltaTime));

        ScreenManager.WrapAroundScreen(transform, 18.0f, 14.0f);
        DestroyAfterCountdown();
    }

    private void DestroyAfterCountdown()
    {
        if (_destroyTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
