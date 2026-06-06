using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _bulletObject;

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
            Instantiate(_bulletObject, new Vector3(transform.position.x, transform.position.y), transform.rotation);

            _cooldownTimer = _secondsBetweenShots;
        }
    }
}
