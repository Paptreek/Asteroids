using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShipCannon : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _cannonTransform;

    private InputAction _attackAction;
    private Vector3 _cannonPosition;
    private float _secondsBetweenShots = 0.50f;
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
            _cannonPosition = _cannonTransform.position;

            Bullet bullet = Instantiate(_bullet, _cannonPosition, transform.rotation);
            bullet.SetFiringShipSpeed(this);
            bullet.SetFiringShip(FiringShip.Player);

            _cooldownTimer = _secondsBetweenShots;
        }
    }
}
