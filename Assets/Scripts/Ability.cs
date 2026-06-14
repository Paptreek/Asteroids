using UnityEngine;
using UnityEngine.InputSystem;

public class Ability : MonoBehaviour
{
    [SerializeField] private ParticleSystem _warpEffect;
    
    private InputAction _warp;

    private void Awake()
    {
        _warp = InputSystem.actions.FindAction("Warp");
    }

    private void Update()
    {
        if (_warp.WasPressedThisFrame())
        {
            Warp();
        }
    }

    private void Warp()
    {
        Instantiate(_warpEffect, transform.position, Quaternion.identity);

        float newPositionX = Random.Range(-18.0f, 18.0f);
        float newPositionY = Random.Range(-14.0f, 14.0f);

        transform.position = new Vector3(newPositionX, newPositionY);
    }
}
