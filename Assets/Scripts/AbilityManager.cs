using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _warpEffect;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _playerShield;

    private float _multiShotTimer;
    private float _shieldTimer;
    private float _piercingAmmoTimer;
    private InputAction _warp;
    private InputAction _useAbility;

    public int WarpUses { get; set; } = 1;
    public bool HasMultiShot { get; set; }
    public bool MultiShotActivated { get; set; }
    public bool HasShield { get; set; }
    public bool ShieldActivated { get; set; }
    public bool HasPiercingAmmo { get; set; }
    public bool PiercingAmmoActivated { get; set; }

    private void Awake()
    {
        _warp = InputSystem.actions.FindAction("Warp");
        _useAbility = InputSystem.actions.FindAction("UseAbility");
    }

    private void Update()
    {
        _multiShotTimer -= Time.deltaTime;
        _shieldTimer -= Time.deltaTime;
        _piercingAmmoTimer -= Time.deltaTime;
        
        if (_warp.WasPressedThisFrame())
        {
            Warp();
        }

        ManageMultiShotPowerUp();
        ManageShieldPowerUp();
        ManagePiercingPowerUp();
    }

    public void ClearPowerUps()
    {
        HasMultiShot = false;
        HasShield = false;
        HasPiercingAmmo = false;
    }
    
    private void Warp()
    {
        if (WarpUses > 0 && !_player.IsDead)
        {
            Instantiate(_warpEffect, transform.position, Quaternion.identity);

            float newPositionX = Random.Range(-18.0f, 18.0f);
            float newPositionY = Random.Range(-14.0f, 14.0f);

            _player.transform.position = new Vector3(newPositionX, newPositionY);

            Instantiate(_warpEffect, _player.transform.position, Quaternion.identity);

            WarpUses--;
        }
    }

    private void ManageMultiShotPowerUp()
    {
        if (HasMultiShot && _useAbility.WasPressedThisFrame())
        {
            MultiShotActivated = true;
            HasMultiShot = false;
            _multiShotTimer = 5.0f;
        }

        if (_multiShotTimer <= 0)
        {
            MultiShotActivated = false;
        }
    }

    private void ManageShieldPowerUp()
    {
        if (HasShield && _useAbility.WasPressedThisFrame())
        {
            _playerShield.SetActive(true);
            _player.GetComponent<PolygonCollider2D>().enabled = false;
            ShieldActivated = true;
            HasShield = false;
            _shieldTimer = 1.5f;
        }

        if (ShieldActivated)
        {
            _playerShield.transform.position = _player.transform.position;
        }

        if (_shieldTimer <= 0)
        {
            ShieldActivated = false;
            _playerShield.SetActive(false);

            if (_player.EnableCollisionTimer <= 0)
            {
                _player.GetComponent<PolygonCollider2D>().enabled = true;
            }
        }
    }

    private void ManagePiercingPowerUp()
    {
        if (HasPiercingAmmo && _useAbility.WasPressedThisFrame())
        {
            PiercingAmmoActivated = true;
            HasPiercingAmmo = false;
            _piercingAmmoTimer = 5.0f;
        }

        if (_piercingAmmoTimer <= 0)
        {
            PiercingAmmoActivated = false;
        }
    }
}
