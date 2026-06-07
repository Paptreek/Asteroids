using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;

    private InputAction _attackAction;
    private Rigidbody2D _rb;
    private float _secondsBetweenShots = 0.25f;
    private float _cooldownTimer;

    private void Awake()
    {
        _attackAction = InputSystem.actions.FindAction("Attack");
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _cooldownTimer -= Time.deltaTime;

        FireBullet();
    }

    private void FireBullet()
    {
        if (_attackAction.IsPressed() && _cooldownTimer <= 0)
        {
            GameObject bullet = _bulletPrefab;
            bullet.GetComponent<Bullet>().FiringShipSpeed = Mathf.Abs(_rb.linearVelocityY);

            Instantiate(bullet, new Vector3(transform.position.x, transform.position.y), transform.rotation);

            _cooldownTimer = _secondsBetweenShots;
        }
    }
}
