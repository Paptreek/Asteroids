using UnityEngine;
using UnityEngine.InputSystem;

public class Ability : MonoBehaviour
{
    [SerializeField] private ParticleSystem _warpEffect;

    private int _warpUses = 1;
    private float _abilityTimer;
    private InputAction _warp;

    public bool HasMultiShot { get; private set; }

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

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            HasMultiShot = true;
            _abilityTimer = 5.0f;
        }

        if (_abilityTimer <= 0)
        {
            HasMultiShot = false;
        }
    }

    private void Warp()
    {
        if (_warpUses > 0)
        {
            Instantiate(_warpEffect, transform.position, Quaternion.identity);

            float newPositionX = Random.Range(-18.0f, 18.0f);
            float newPositionY = Random.Range(-14.0f, 14.0f);

            transform.position = new Vector3(newPositionX, newPositionY);

            Instantiate(_warpEffect, transform.position, Quaternion.identity);

            _warpUses--;
        }
    }
}
