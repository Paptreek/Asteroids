using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyShipSpawner : MonoBehaviour
{
    [SerializeField] private EnemyShip _enemyShip;
    [SerializeField] private Player _player;

    private float _spawnTimerSmall = 45.0f;
    private float _spawnTimerLarge = 30.0f;

    private void Update()
    {
        _spawnTimerLarge -= Time.deltaTime;
        _spawnTimerSmall -= Time.deltaTime;

        SpawnEnemyShip();
    }

    public void ResetSpawnTimers()
    {
        _spawnTimerSmall = 45.0f;
        _spawnTimerLarge = 30.0f;
    }

    private void SpawnEnemyShip()
    {
        if (_spawnTimerSmall <= 0)
        //if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            EnemyShip enemyShip = Instantiate(_enemyShip);
            enemyShip.SetShipSize(EnemyShip.ShipSize.Small);
            enemyShip.SetPlayerShip(_player);

            _spawnTimerSmall = 15.0f; // these should be getting set from GameManager on a per-round basis
        }

        if (_spawnTimerLarge <= 0)
        //if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            EnemyShip enemyShip = Instantiate(_enemyShip);
            enemyShip.SetShipSize(EnemyShip.ShipSize.Large);
            enemyShip.SetPlayerShip(_player);

            _spawnTimerLarge = 10.0f; // these should be getting set from GameManager on a per-round basis
        }
    }
}
