using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private AsteroidManager _asteroidManager;
    [SerializeField] private AsteroidSpawner _asteroidSpawner;

    private int _round = 1;
    private bool _playerHasWon;

    private void Start()
    {
        _asteroidSpawner.SpawnNewRound(1, Asteroid.Size.Small, _asteroidManager.Asteroids);
    }

    private void Update()
    {
        CheckToStartNewRound();
        CheckForGameOver();
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
                _asteroidSpawner.SpawnNewRound(1, Asteroid.Size.Small, _asteroidManager.Asteroids);
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
}
