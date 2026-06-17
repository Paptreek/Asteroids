using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private AsteroidManager _asteroidManager;
    [SerializeField] private AsteroidSpawner _asteroidSpawner;
    [SerializeField] private EnemyShipSpawner _enemyShipSpawner;
    [SerializeField] private AbilityManager _abilityManager;
    [SerializeField] private GameObject _gameOverPrefab;
    [SerializeField] private GameObject _upgradePanel;
    [SerializeField] private GameObject _upgradePanelBackground;
    [SerializeField] private UpgradeManager _upgradeManager;
    
    private bool _playerHasWon;
    private bool _playerHasLost;
    private bool _gameOverCreated;
    private int _round = 1;
    private int _score;
    private int _bonusScore;
    private float _roundTimer = 120.0f;
    private float _spawnTimerSmall = 45.0f;
    private float _spawnTimerLarge = 30.0f;

    // these are for dev mode
    private InputAction _enterDevMode;
    private int _asteroidsToSpawnStart = 4;
    private int _asteroidsToSpawnRound = 3;
    private Asteroid.Size _asteroidSize = Asteroid.Size.Large;
    
    private void Awake()
    {
        _enterDevMode = InputSystem.actions.FindAction("EnterDevMode");
    }

    private void Start()
    {
        _asteroidSpawner.SpawnNewRound(_asteroidsToSpawnStart, _asteroidSize, _asteroidManager.Asteroids);
        _enemyShipSpawner.SetSpawnTimers(_spawnTimerSmall, _spawnTimerLarge);
    }

    private void Update()
    {
        _roundTimer -= Time.deltaTime;

        CheckToStartNewRound();
        CheckForGameOver();

        CheckForDevMode();
    }

    private void CheckToStartNewRound()
    {
        int maxRounds = 5;

        if (_asteroidManager.Asteroids.Count <= 0 && !_playerHasLost)
        {
            if (_round < maxRounds)
            {
                GetPlayerUpgradeChoice();
                StartNextRound();
            }
            else
            {
                _playerHasWon = true;
            }
        }
    }

    private void CheckForGameOver()
    {
        if (_player == null)
        {
            Debug.Log($"You died. Game over!");
            _playerHasLost = true;
            _enemyShipSpawner.enabled = false;
            _asteroidSpawner.enabled = false;

            if (!_gameOverCreated)
            {
                Instantiate(_gameOverPrefab);
                _gameOverCreated = true;
            }
        }

        if (_playerHasWon)
        {
            Debug.Log($"All rounds completed. You win!");
            _enemyShipSpawner.enabled = false;
            _asteroidSpawner.enabled = false;
        }

    }

    private int GetScore()
    {
        int pointsForLargeShips = _player.LargeShipsDestroyed * 25;
        int pointsForSmallShips = _player.SmallShipsDestroyed * 50;
        int pointsForShips = pointsForLargeShips + pointsForSmallShips;
        
        int pointsForLargeAsteroids = _asteroidManager.LargeAsteroidsDestroyed * 5;
        int pointsForMediumAsteroids = _asteroidManager.MediumAsteroidsDestroyed * 10;
        int pointsForSmallAsteroids = _asteroidManager.SmallAsteroidsDestroyed * 25;
        int pointsForAsteroids = pointsForLargeAsteroids + pointsForMediumAsteroids + pointsForSmallAsteroids;

        return _score = _bonusScore + pointsForShips + pointsForAsteroids;
    }

    private void StartNextRound()
    {
        AddBonusScore();

        _round++;
        _spawnTimerSmall -= 2.5f;
        _spawnTimerLarge -= 2.5f;

        Debug.Log($"Round complete. Starting round {_round}! Spawn Timers: SM {_spawnTimerSmall}, LG {_spawnTimerLarge}");

        DestroyAllEnemyShips();

        _roundTimer = 120.0f;
        _player.ResetPosition();
        _abilityManager.WarpUses = _abilityManager.MaxWarpUses;
        _enemyShipSpawner.SetSpawnTimers(_spawnTimerSmall, _spawnTimerLarge);

        if (_player != null)
        {
            _enemyShipSpawner.EnemyShips.Clear();
            _asteroidSpawner.SpawnNewRound(_asteroidsToSpawnRound + _round, _asteroidSize, _asteroidManager.Asteroids);
        }
    }

    private void AddBonusScore()
    {
        int pointsToAdd = Mathf.RoundToInt(_roundTimer) * 5;

        Debug.Log($"Time Left: {_roundTimer}, Points Added: {pointsToAdd}");

        _bonusScore += pointsToAdd;
    }

    private void DestroyAllEnemyShips()
    {
        foreach (EnemyShip enemyShip in _enemyShipSpawner.EnemyShips)
        {
            if (enemyShip != null)
            {
                Destroy(enemyShip.gameObject);
            }
        }
        
        _enemyShipSpawner.EnemyShips.Clear();

        Debug.Log($"Ships cleared. # of ships: {_enemyShipSpawner.EnemyShips.Count}");
    }

    private void GetPlayerUpgradeChoice()
    {
        Time.timeScale = 0;

        _upgradePanelBackground.SetActive(true);
        _upgradePanel.SetActive(true);

        _upgradeManager.ShuffleUpgrades(_upgradeManager.UpgradeOptions);
        _upgradeManager.AssignRandomUpgrades();
        _upgradeManager.DisplayUpgrades();
    }
    
    // this is for dev mode
    private void CheckForDevMode()
    {
        if (_enterDevMode.WasPerformedThisFrame())
        {
            _asteroidsToSpawnStart = 1;
            _asteroidsToSpawnRound = 1;
            _asteroidSize = Asteroid.Size.Small;
            Debug.Log("DevMode Enabled");
        }
    }
}
