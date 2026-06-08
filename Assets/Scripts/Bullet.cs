using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _destroyTimer = 0.75f;
    private float _baseSpeed = 25;
    private float _firingShipSpeed;

    private void Update()
    {
        _destroyTimer -= Time.deltaTime;

        transform.Translate(new Vector2(0, (_firingShipSpeed + _baseSpeed) * Time.deltaTime));

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

    public void SetFiringShipSpeed(PlayerAttack firingShip)
    {
        Rigidbody2D firingShipRB = firingShip.GetComponent<Rigidbody2D>();

        float firingShipSpeedX = Mathf.Abs(firingShipRB.linearVelocityX);
        float firingShipSpeedY = Mathf.Abs(firingShipRB.linearVelocityY);

        _firingShipSpeed = Mathf.Max(firingShipSpeedX, firingShipSpeedY);
    }
}
