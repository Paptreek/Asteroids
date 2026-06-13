using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private AsteroidManager _asteroidManager;
    [SerializeField] private AsteroidSpawner _asteroidSpawner;
    [SerializeField] private EnemyShipSpawner _enemyShipSpawner;

    private bool _playerHasWon;
    private int _round = 1;
    private int _score;
    private int _bonusScore;
    private float _roundTimer = 120.0f;

    private void Start()
    {
        _asteroidSpawner.SpawnNewRound(4, Asteroid.Size.Large, _asteroidManager.Asteroids);
    }

    private void Update()
    {
        _roundTimer -= Time.deltaTime;

        CheckToStartNewRound();
        CheckForGameOver();

        Debug.Log($"Score: {GetScore()}");
    }

    private void CheckToStartNewRound()
    {
        int maxRounds = 5;

        if (_asteroidManager.Asteroids.Count <= 0)
        {
            if (_round < maxRounds)
            {
                ResetRound();

                if (_player != null)
                {
                    _asteroidSpawner.SpawnNewRound(3 + _round, Asteroid.Size.Large, _asteroidManager.Asteroids);
                }
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
        }

        if (_playerHasWon)
        {
            Debug.Log($"All rounds completed. You win!");
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

    private void ResetRound()
    {
        AddBonusScore();

        _round++;
        Debug.Log($"Round complete. Starting round {_round}!");

        _roundTimer = 120.0f;
        _player.ResetPosition();
        _enemyShipSpawner.ResetSpawnTimers();
    }

    private void AddBonusScore()
    {
        int pointsToAdd = Mathf.RoundToInt(_roundTimer) * 5;

        Debug.Log($"Time Left: {_roundTimer}, Points Added: {pointsToAdd}");

        _bonusScore += pointsToAdd;
    }
}
