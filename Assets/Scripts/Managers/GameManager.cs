using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private AsteroidManager _asteroidManager;
    [SerializeField] private AsteroidSpawner _asteroidSpawner;
    [SerializeField] private EnemyShipSpawner _enemyShipSpawner;
    [SerializeField] private PowerUpManager _powerUpManager;
    [SerializeField] private GameObject _upgradePanel;
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private Boss _boss;
    [SerializeField] private GameObject _gameOverManagerObj;
    
    private bool _playerHasWon;
    private bool _bossActivated;
    private bool _gameOverActivated;
    private int _round = 1;
    private float _roundTimer = 120.0f;
    private float _playTimeCounter;
    private float _spawnTimerSmall = 45.0f;
    private float _spawnTimerLarge = 30.0f;
    private BossSpawnSequence _bossSpawnSequence;

    public int Score { get; private set; }
    public int TotalPlayTime { get; private set; }
    public int BonusTimeScore { get; private set; }
    public int PointsFromAsteroids { get; private set; }
    public int PointsFromShips { get; private set; }

    // these are for dev mode
    private InputAction _enterDevMode;
    private int _asteroidsToSpawnStart = 4;
    private int _asteroidsToSpawnRound = 3;
    private Asteroid.Size _asteroidSize = Asteroid.Size.Large;
    private bool _bossTestModeEnabled = false;
    
    private void Awake()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        _enterDevMode = InputSystem.actions.FindAction("EnterDevMode");
        _bossSpawnSequence = _boss.GetComponent<BossSpawnSequence>();
    }

    private void Start()
    {
        if (!_bossTestModeEnabled) // remove after testing is done
        {
            _asteroidSpawner.SpawnNewRound(_asteroidsToSpawnStart, _asteroidSize, _asteroidManager.Asteroids);
            _enemyShipSpawner.SetSpawnTimers(_spawnTimerSmall, _spawnTimerLarge);
        }
        else
        {
            EnableBossTestingMode();
        }
    }

    private void Update()
    {
        _roundTimer -= Time.deltaTime;
        _playTimeCounter += Time.deltaTime;

        CheckToStartNewRound();
        CheckForGameOver();

        CalculateScore();

        CheckForDevMode();

        if (_bossActivated && !_bossSpawnSequence.SpawnSequenceComplete)
        {
            _player.gameObject.SetActive(false);
        }
        
        if (_player != null)
        {
            if (!_player.gameObject.activeInHierarchy && _bossActivated && _bossSpawnSequence.SpawnSequenceComplete)
            {
                _player.gameObject.SetActive(true);
                _player.ActivateIFrames(2);
            }
        }
    }

    public int GetDestroyedAsteroidCount()
    {
        return _asteroidManager.LargeAsteroidsDestroyed + _asteroidManager.MediumAsteroidsDestroyed + _asteroidManager.SmallAsteroidsDestroyed;
    }

    public int GetDestroyedShipCount()
    {
        return _player.LargeShipsDestroyed + _player.SmallShipsDestroyed;
    }

    public int GetPlayerDeathCount() => _player.DeathCount;

    public int GetBonusDeathScore()
    {
        int bonusScore = _player.DeathCount switch
        {
            0 => 9999,
            1 => 7500,
            2 => 5000,
            3 => 2000,
            4 => 1000,
            5 => 500,
            _ => 0
        };

        return bonusScore;
    }

    private void CheckToStartNewRound()
    {
        int maxRounds = 5;

        if (!_bossTestModeEnabled) // remove after done testing
        {
            if (_asteroidManager.Asteroids.Count <= 0)
            {
                if (_round < maxRounds)
                {
                    _upgradeManager.SetRoundText($"[ NEXT: ROUND {_round + 1} / {maxRounds} ]");
                    StartNextRound();
                }
                else
                {
                    _upgradeManager.SetRoundText($"[ NEXT: BOSS BATTLE ]");
                    ActivateBoss();
                }
            }

            if (_boss.IsDead)
            {
                _playerHasWon = true;
             
                if (Score > PlayerPrefs.GetInt("HighScore"))
                {
                    SetHighScore();
                }
            }
        }
    }

    private void CheckForGameOver()
    {
        if (_playerHasWon && !_gameOverActivated)
        {
            Debug.Log($"All rounds completed. You win!");

            IncreaseTotalPlayTime();
            DestroyAllEnemyShips();
            _enemyShipSpawner.enabled = false;
            _asteroidSpawner.enabled = false;

            _gameOverManagerObj.SetActive(true);

            _gameOverActivated = true;
        }
    }

    private void CalculateScore()
    {
        int pointsForLargeShips = _player.LargeShipsDestroyed * 25;
        int pointsForSmallShips = _player.SmallShipsDestroyed * 50;
        PointsFromShips = pointsForLargeShips + pointsForSmallShips;
        
        int pointsForLargeAsteroids = _asteroidManager.LargeAsteroidsDestroyed * 5;
        int pointsForMediumAsteroids = _asteroidManager.MediumAsteroidsDestroyed * 10;
        int pointsForSmallAsteroids = _asteroidManager.SmallAsteroidsDestroyed * 25;
        PointsFromAsteroids = pointsForLargeAsteroids + pointsForMediumAsteroids + pointsForSmallAsteroids;

        int pointsFromBoss = _boss.PointsToAdd();

        if (_playerHasWon)
        {
            Score = PointsFromShips + PointsFromAsteroids + pointsFromBoss + BonusTimeScore + GetBonusDeathScore();
        }
        else
        {
            Score = PointsFromShips + PointsFromAsteroids + pointsFromBoss + BonusTimeScore;
        }
    }

    private void AddBonusTimeScore()
    {
        int pointsToAddFromTimer = Mathf.RoundToInt(_roundTimer) * 5;

        Debug.Log($"Time Left: {_roundTimer}, Points Added: {pointsToAddFromTimer}");

        BonusTimeScore += pointsToAddFromTimer;
    }

    private void IncreaseTotalPlayTime()
    {
        TotalPlayTime += Mathf.RoundToInt(_playTimeCounter);
        Debug.Log($"Total Play Time Increased: {TotalPlayTime}");

        _playTimeCounter = 0;
    }

    private void SetHighScore() => PlayerPrefs.SetInt("HighScore", Score);

    private void StartNextRound()
    {
        AddBonusTimeScore();
        IncreaseTotalPlayTime();

        GetPlayerUpgradeChoice();

        _round++;
        _spawnTimerSmall -= 2.5f;
        _spawnTimerLarge -= 2.5f;

        Debug.Log($"Round complete. Starting round {_round}! Spawn Timers: SM {_spawnTimerSmall}, LG {_spawnTimerLarge}");

        DestroyAllEnemyShips();

        _roundTimer = 120.0f;
        _player.ResetPosition(Vector3.zero);
        _powerUpManager.WarpUses = _powerUpManager.MaxWarpUses;
        _enemyShipSpawner.SetSpawnTimers(_spawnTimerSmall, _spawnTimerLarge);

        if (_player != null)
        {
            _enemyShipSpawner.EnemyShips.Clear();
            _asteroidSpawner.SpawnNewRound(_asteroidsToSpawnRound + _round, _asteroidSize, _asteroidManager.Asteroids);
        }
    }

    private void ActivateBoss()
    {
        if (!_bossActivated)
        {
            AddBonusTimeScore();
            IncreaseTotalPlayTime();

            GetPlayerUpgradeChoice();

            _spawnTimerSmall -= 2.5f;
            _spawnTimerLarge -= 2.5f;

            DestroyAllEnemyShips();
            _powerUpManager.WarpUses = _powerUpManager.MaxWarpUses;
            _enemyShipSpawner.SetSpawnTimers(_spawnTimerSmall, _spawnTimerLarge);

            _player.ResetPosition(new Vector3(0, -6.5f, 0));
            _boss.gameObject.SetActive(true);
            _bossActivated = true;
        }
    }

    private void DestroyAllEnemyShips()
    {
        if (_enemyShipSpawner.EnemyShips.Count > 0)
        {
            foreach (EnemyShip enemyShip in _enemyShipSpawner.EnemyShips)
            {
                if (enemyShip != null)
                {
                    enemyShip.DestroyShip();
                }
            }
        
            _enemyShipSpawner.EnemyShips.Clear();
        }
    }

    private void GetPlayerUpgradeChoice()
    {
        Time.timeScale = 0;

        _upgradePanel.SetActive(true);

        _upgradeManager.ShuffleUpgrades(_upgradeManager.UpgradeOptions);
        _upgradeManager.AssignRandomUpgrades();
        _upgradeManager.DisplayUpgrades();
    }
    
    // everything below is for testing purposes
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

    private void EnableBossTestingMode()
    {
        _asteroidSpawner.enabled = false;
        _enemyShipSpawner.enabled = false;

        _player.transform.position = new Vector3(0, -6.5f, 0f);
    }
    // everything above is for testing purposes
}
