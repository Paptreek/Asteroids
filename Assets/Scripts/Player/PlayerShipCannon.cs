using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShipCannon : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _cannonTransformCenter;
    [SerializeField] private Transform _cannonTransformLeft;
    [SerializeField] private Transform _cannonTransformRight;
    [SerializeField] private AbilityManager _abilityManager;

    private InputAction _attackAction;
    private Player _player;
    private Vector3 _cannonPositionCenter;
    private Vector3 _cannonPositionLeft;
    private Vector3 _cannonPositionRight;
    private float _secondsBetweenShots = 0.50f;
    private float _cooldownTimer;

    private void Awake()
    {
        _attackAction = InputSystem.actions.FindAction("Attack");
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        _cooldownTimer -= Time.deltaTime;

        FireWeapon();
    }

    private void FireWeapon()
    {
        _cannonPositionCenter = _cannonTransformCenter.position;
        _cannonPositionLeft = _cannonTransformLeft.position;
        _cannonPositionRight = _cannonTransformRight.position;

        if (_attackAction.IsPressed() && _cooldownTimer <= 0 && !_player.IsDead)
        {
            Bullet bulletCenter = Instantiate(_bullet, _cannonPositionCenter, transform.rotation);
            bulletCenter.SetFiringShipSpeed(this);
            bulletCenter.SetFiringShip(FiringShip.Player);

            if (_abilityManager.PiercingAmmoActivated)
            {
                bulletCenter.PiercingAmmoActivated = true;
            }

            if (_abilityManager.MultiShotActivated)
            {
                Bullet bulletLeft = Instantiate(_bullet, _cannonPositionLeft, _cannonTransformLeft.rotation);
                bulletLeft.SetFiringShipSpeed(this);
                bulletLeft.SetFiringShip(FiringShip.Player);

                Bullet bulletRight = Instantiate(_bullet, _cannonPositionRight, _cannonTransformRight.rotation);
                bulletRight.SetFiringShipSpeed(this);
                bulletRight.SetFiringShip(FiringShip.Player);

                if (_abilityManager.PiercingAmmoActivated)
                {
                    bulletLeft.PiercingAmmoActivated = true;
                    bulletRight.PiercingAmmoActivated = true;
                }
            }
            
            _cooldownTimer = _secondsBetweenShots;
        }
    }
}
