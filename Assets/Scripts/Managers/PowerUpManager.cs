using UnityEngine;
using UnityEngine.InputSystem;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _warpEffect;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _playerShield;
    [SerializeField] private GameObject _multishotCannonSpriteObj;
    [SerializeField] private PowerUp _powerUp;

    private float _multiShotTimer;
    private float _shieldTimer;
    private float _piercingAmmoTimer;
    private InputAction _warp;
    private InputAction _useAbility;

    public int MaxWarpUses { get; private set; } = 1;
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
        
        if (_player != null)
        {
            if (_warp.WasPressedThisFrame())
            {
                Warp();
            }

            ManageMultiShotPowerUp();
            ManageShieldPowerUp();
            ManagePiercingPowerUp();
        }
    }

    public void IncreaseMaxWarpUses()
    {
        MaxWarpUses++;
        WarpUses = MaxWarpUses;
    }

    public void MaybeDropPowerUp(Asteroid asteroid)
    {
        //int randomNumber = Random.Range(0, 13);
        int randomNumber = Random.Range(0, 2);

        //if (randomNumber == 12)
        if (randomNumber == 1)
        {
            PowerUp powerUp = Instantiate(_powerUp, asteroid.transform.position, Quaternion.identity);
            powerUp.SetAbilityManager(this);
        }

        Debug.Log($"PowerUp Drop Roll: {randomNumber} / 12");
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
            
            _multishotCannonSpriteObj.SetActive(true);
        }

        if (_multiShotTimer <= 0)
        {
            MultiShotActivated = false;
        }

        if (_player != null)
        {
            if (MultiShotActivated && _player.GetComponent<SpriteRenderer>().enabled)
            {
                _multishotCannonSpriteObj.SetActive(true);
            }
            else
            {
                _multishotCannonSpriteObj.SetActive(false);
            }
        }
    }

    private void ManageShieldPowerUp()
    {
        if (HasShield && _useAbility.WasPressedThisFrame())
        {
            _playerShield.SetActive(true);

            ShieldActivated = true;
            HasShield = false;
            _shieldTimer = 2.5f;
        }

        if (ShieldActivated)
        {
            _playerShield.transform.position = _player.transform.position;
        }

        if (_shieldTimer <= 0)
        {
            ShieldActivated = false;
            _playerShield.SetActive(false);
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
