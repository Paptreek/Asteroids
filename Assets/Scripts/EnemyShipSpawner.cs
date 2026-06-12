using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyShipSpawner : MonoBehaviour
{
    [SerializeField] private EnemyShip _enemyShip;
    [SerializeField] private Player _player;

    private float _initialSpawnTimerLarge = 30.0f;
    private float _initialSpawnTimerSmall = 45.0f;

    private void Update()
    {
        _initialSpawnTimerLarge -= Time.deltaTime;
        _initialSpawnTimerSmall -= Time.deltaTime;

        SpawnEnemyShip();
    }

    private void SpawnEnemyShip()
    {
        //if (_initialSpawnTimerSmall <= 0)
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            EnemyShip enemyShip = Instantiate(_enemyShip);
            enemyShip.SetShipSize(EnemyShip.ShipSize.Small);
            enemyShip.SetPlayerShip(_player);

            _initialSpawnTimerSmall = 15.0f; // these should be getting set from GameManager on a per-round basis
        }

        //if (_initialSpawnTimerLarge <= 0)
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            EnemyShip enemyShip = Instantiate(_enemyShip);
            enemyShip.SetShipSize(EnemyShip.ShipSize.Large);
            enemyShip.SetPlayerShip(_player);

            _initialSpawnTimerLarge = 10.0f; // these should be getting set from GameManager on a per-round basis
        }
    }
}
