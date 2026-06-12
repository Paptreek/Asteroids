using UnityEngine;

public class Bullet : MonoBehaviour
{
    private FiringShip _firingShip;
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
        if (_firingShip == FiringShip.Player)
        {
            if (collision.CompareTag("Asteroid") || collision.CompareTag("EnemyShip")) // for some reason "EnemyShip" breaks the enemy firing
            {
                Destroy(gameObject);
            }
        }
        
        if (_firingShip == FiringShip.Enemy)
        {
            if (collision.CompareTag("Asteroid") || collision.CompareTag("Player")) // but "Player" does not break player firing...
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetFiringShipSpeed(PlayerShipCannon firingShip)
    {
        Rigidbody2D firingShipRB = firingShip.GetComponent<Rigidbody2D>();

        float firingShipSpeedX = Mathf.Abs(firingShipRB.linearVelocityX);
        float firingShipSpeedY = Mathf.Abs(firingShipRB.linearVelocityY);

        _firingShipSpeed = Mathf.Max(firingShipSpeedX, firingShipSpeedY);
    }

    public void SetFiringShip(FiringShip firingShip)
    {
        if (firingShip == FiringShip.Player)
        {
            tag = "PlayerBullet";
        }
        else if (firingShip == FiringShip.Enemy)
        {
            tag = "EnemyBullet";
        }
    }

    public void SetFiringDirection(float firingDirection)
    {
        transform.eulerAngles = new Vector3(0, 0, firingDirection);
    }
}

public enum FiringShip { Player, Enemy }