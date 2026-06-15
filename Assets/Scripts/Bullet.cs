using UnityEngine;

public class Bullet : MonoBehaviour
{
    private FiringShip _firingShip;
    private float _destroyTimer = 0.75f;
    private float _baseSpeed = 25;
    private float _firingShipSpeed;

    public bool PiercingAmmoActivated { get; set; }

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
            if (!PiercingAmmoActivated && collision.CompareTag("Asteroid") || collision.CompareTag("EnemyShip"))
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (collision.CompareTag("Asteroid") || collision.CompareTag("Player"))
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
            _firingShip = FiringShip.Player;
            tag = "PlayerBullet";
        }
        else if (firingShip == FiringShip.Enemy)
        {
            _firingShip = FiringShip.Enemy;
            tag = "EnemyBullet";
        }
    }

    public void SetFiringDirection(float firingDirection)
    {
        transform.eulerAngles = new Vector3(0, 0, firingDirection);
    }
}

public enum FiringShip { Player, Enemy }