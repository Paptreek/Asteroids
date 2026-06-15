using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _warpEffect;
    [SerializeField] private Player _player;

    private int _warpUses = 1;
    private float _abilityTimer;
    private InputAction _warp;

    public bool HasMultiShot { get; set; }
    public bool MultiShotActivated { get; set; }

    private void Awake()
    {
        _warp = InputSystem.actions.FindAction("Warp");
    }

    private void Update()
    {
        _abilityTimer -= Time.deltaTime;
        
        if (_warp.WasPressedThisFrame())
        {
            Warp();
        }

        if (HasMultiShot && Keyboard.current.eKey.wasPressedThisFrame)
        {
            MultiShotActivated = true;
            HasMultiShot = false;
            _abilityTimer = 5.0f;
        }

        if (_abilityTimer <= 0)
        {
            MultiShotActivated = false;
        }
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
}
