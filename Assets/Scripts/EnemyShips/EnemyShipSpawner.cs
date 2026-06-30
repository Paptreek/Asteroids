using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // used for testing

public class EnemyShipSpawner : MonoBehaviour
{
    [SerializeField] private EnemyShip _enemyShip;
    [SerializeField] private Player _player;
    [SerializeField] private PowerUpManager _powerUpManager;

    private float _spawnTimerSmall;
    private float _spawnTimerLarge;

    public List<EnemyShip> EnemyShips { get; set; } = new List<EnemyShip>();

    private void Update()
    {
        _spawnTimerLarge -= Time.deltaTime;
        _spawnTimerSmall -= Time.deltaTime;

        SpawnEnemyShip();
    }

    public void SetSpawnTimers(float spawnTimerSmall, float spawnTimerLarge)
    {
        _spawnTimerSmall = spawnTimerSmall;
        _spawnTimerLarge = spawnTimerLarge;
    }

    private void SpawnEnemyShip()
    {
        if (_spawnTimerSmall <= 0)
        //if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            EnemyShip enemyShip = Instantiate(_enemyShip);
            enemyShip.SetShipSize(EnemyShip.ShipSize.Small);
            enemyShip.SetPlayerShip(_player);
            enemyShip.SetPowerUpManager(_powerUpManager);
            EnemyShips.Add(enemyShip);

            Debug.Log($"Enemy Ship #: {EnemyShips.Count}");

            _spawnTimerSmall = 15.0f;
        }

        if (_spawnTimerLarge <= 0)
        //if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            EnemyShip enemyShip = Instantiate(_enemyShip);
            enemyShip.SetShipSize(EnemyShip.ShipSize.Large);
            enemyShip.SetPlayerShip(_player);
            enemyShip.SetPowerUpManager(_powerUpManager);
            EnemyShips.Add(enemyShip);

            Debug.Log($"Enemy Ship #: {EnemyShips.Count}");

            _spawnTimerLarge = 10.0f;
        }
    }
}
