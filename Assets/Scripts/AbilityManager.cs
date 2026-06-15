using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _warpEffect;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _playerShield;

    private int _warpUses = 1;
    private float _multiShotTimer;
    private float _shieldTimer;
    private InputAction _warp;
    private InputAction _useAbility;

    public bool HasMultiShot { get; set; }
    public bool MultiShotActivated { get; set; }
    public bool HasShield { get; set; }
    public bool ShieldActivated { get; set; }

    private void Awake()
    {
        _warp = InputSystem.actions.FindAction("Warp");
        _useAbility = InputSystem.actions.FindAction("UseAbility");
    }

    private void Update()
    {
        _multiShotTimer -= Time.deltaTime;
        _shieldTimer -= Time.deltaTime;
        
        if (_warp.WasPressedThisFrame())
        {
            Warp();
        }

        ManageMultiShotPowerUp();

        ManageShieldPowerUp();
    }

    private void Warp()
    {
        if (_warpUses > 0 && !_player.IsDead)
        {
            Instantiate(_warpEffect, transform.position, Quaternion.identity);

            float newPositionX = Random.Range(-18.0f, 18.0f);
            float newPositionY = Random.Range(-14.0f, 14.0f);

            _player.transform.position = new Vector3(newPositionX, newPositionY);

            Instantiate(_warpEffect, _player.transform.position, Quaternion.identity);

            _warpUses--;
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

            if (_player.EnableCollisionTimer <= 0)
            {
                _player.GetComponent<PolygonCollider2D>().enabled = true;
            }
        }
    }
}
