using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShipCannon : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private PowerUpManager _powerUpManager;
    [SerializeField] private Transform _cannonTransformCenter;
    [SerializeField] private Transform _cannonTransformLeft;
    [SerializeField] private Transform _cannonTransformRight;
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private AudioClip _multiShotSound;
    [SerializeField] private AudioClip _piercingShotSound;
    [SerializeField] private AudioClip _piercingMultiShotSound;

    private InputAction _attackAction;
    private Player _player;
    private Vector3 _cannonPositionCenter;
    private Vector3 _cannonPositionLeft;
    private Vector3 _cannonPositionRight;
    private float _secondsBetweenShots = 0.50f;
    private float _cooldownTimer;
    
    public float BulletSpeedUpgradeAmount { get; set; }

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

    public void DecreaseTimeBetweenAttacks(float amountToDecrease)
    {
        _secondsBetweenShots -= amountToDecrease;
    }

    private void FireWeapon()
    {
        _cannonPositionCenter = _cannonTransformCenter.position;
        _cannonPositionLeft = _cannonTransformLeft.position;
        _cannonPositionRight = _cannonTransformRight.position;

        if (_attackAction.IsPressed() && _cooldownTimer <= 0 && !_player.IsDead)
        {
            if (_powerUpManager.MultiShotActivated && _powerUpManager.PiercingAmmoActivated)
            {
                AudioManager.Instance.PlayPlayerShoot(_piercingMultiShotSound);
            }
            else if (_powerUpManager.MultiShotActivated)
            {
                AudioManager.Instance.PlayPlayerShoot(_multiShotSound);
            }
            else if (_powerUpManager.PiercingAmmoActivated)
            {
                AudioManager.Instance.PlayPlayerShoot(_piercingShotSound);
            }
            else
            {
                AudioManager.Instance.PlayPlayerShoot(_shootSound);
            }

            Bullet bulletCenter = Instantiate(_bullet, _cannonPositionCenter, transform.rotation);
            bulletCenter.SetFiringShipSpeed(this);
            bulletCenter.SetFiringShip(FiringShip.Player);
            bulletCenter.SetBulletSpeed(BulletSpeedUpgradeAmount);

            if (_powerUpManager.PiercingAmmoActivated)
            {
                bulletCenter.PiercingAmmoActivated = true;
            }

            if (_powerUpManager.MultiShotActivated)
            {
                Bullet bulletLeft = Instantiate(_bullet, _cannonPositionLeft, _cannonTransformLeft.rotation);
                bulletLeft.SetFiringShipSpeed(this);
                bulletLeft.SetFiringShip(FiringShip.Player);
                bulletLeft.SetBulletSpeed(BulletSpeedUpgradeAmount);

                Bullet bulletRight = Instantiate(_bullet, _cannonPositionRight, _cannonTransformRight.rotation);
                bulletRight.SetFiringShipSpeed(this);
                bulletRight.SetFiringShip(FiringShip.Player);
                bulletRight.SetBulletSpeed(BulletSpeedUpgradeAmount);

                if (_powerUpManager.PiercingAmmoActivated)
                {
                    bulletLeft.PiercingAmmoActivated = true;
                    bulletRight.PiercingAmmoActivated = true;
                }
            }
            
            _cooldownTimer = _secondsBetweenShots;
        }
    }
}
