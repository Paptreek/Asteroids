using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private AsteroidManager _asteroidManager;
    [SerializeField] private AsteroidSpawner _asteroidSpawner;

    private int _round = 1;
    private int _score;
    private bool _playerHasWon;

    private void Start()
    {
        _asteroidSpawner.SpawnNewRound(1, Asteroid.Size.Small, _asteroidManager.Asteroids);
    }

    private void Update()
    {
        CheckToStartNewRound();
        CheckForGameOver();

        Debug.Log($"Score: {GetCurrentScore()}");
    }

    private void CheckToStartNewRound()
    {
        int maxRounds = 5;

        if (_asteroidManager.Asteroids.Count <= 0)
        {
            if (_round < maxRounds)
            {
                _round++;
                Debug.Log($"Round complete. Starting round {_round}!");
                _player.ResetPosition();

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

    private int GetCurrentScore()
    {
        int pointsForLarge = _asteroidManager.LargeAsteroidsDestroyed * 50;
        int pointsForMedium = _asteroidManager.MediumAsteroidsDestroyed * 100;
        int pointsForSmall = _asteroidManager.SmallAsteroidsDestroyed * 250;

        return _score = pointsForLarge + pointsForMedium + pointsForSmall;
    }
}
