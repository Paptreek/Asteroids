using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private AsteroidManager _asteroidManager;
    [SerializeField] private AsteroidSpawner _asteroidSpawner;

    private bool _playerHasWon;
    private int _round = 1;
    private int _score;
    private int _bonusScore;
    private float _roundTimer = 120.0f;

    private void Start()
    {
        _asteroidSpawner.SpawnNewRound(1, Asteroid.Size.Small, _asteroidManager.Asteroids);
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
        int pointsForLarge = _asteroidManager.LargeAsteroidsDestroyed * 50;
        int pointsForMedium = _asteroidManager.MediumAsteroidsDestroyed * 100;
        int pointsForSmall = _asteroidManager.SmallAsteroidsDestroyed * 250;
        int baseScore = pointsForLarge + pointsForMedium + pointsForSmall;

        return _score = _bonusScore + baseScore;
    }

    private void ResetRound()
    {
        AddBonusScore();

        _round++;
        Debug.Log($"Round complete. Starting round {_round}!");

        _roundTimer = 120.0f;
        _player.ResetPosition();
    }

    private void AddBonusScore()
    {
        int pointsToAdd = Mathf.RoundToInt(_roundTimer) * 25;

        Debug.Log($"Time Left: {_roundTimer}, Points Added: {pointsToAdd}");

        _bonusScore += pointsToAdd;
    }
}
