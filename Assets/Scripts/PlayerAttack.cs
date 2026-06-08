using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;

    private InputAction _attackAction;
    private float _secondsBetweenShots = 0.25f;
    private float _cooldownTimer;

    private void Awake()
    {
        _attackAction = InputSystem.actions.FindAction("Attack");
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
            Bullet bullet = Instantiate(_bulletPrefab, new Vector3(transform.position.x, transform.position.y), transform.rotation);
            bullet.SetFiringShipSpeed(this);

            _cooldownTimer = _secondsBetweenShots;
        }
    }
}
