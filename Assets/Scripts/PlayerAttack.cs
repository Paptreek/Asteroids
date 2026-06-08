using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;

    private InputAction _attackAction;
    private float _secondsBetweenShots = 0.25f;
    private float _cooldownTimer;

    private GameObject _testObj;

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
            Vector3 spawnPosition = transform.position;

            Bullet bullet = Instantiate(_bulletPrefab, spawnPosition, transform.rotation);
            bullet.SetFiringShipSpeed(this);

            _cooldownTimer = _secondsBetweenShots;
        }
    }
}
