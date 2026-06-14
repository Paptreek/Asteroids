using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private AsteroidSpawner _asteroidSpawner;

    private List<Asteroid> _asteroids = new List<Asteroid>();

    private void Start()
    {
        SpawnAsteroids();
    }

    private void Update()
    {
        StartGame();
    }

    private void SpawnAsteroids()
    {
        int largeAsteroidsToSpawn = Random.Range(3, 5);
        int mediumAsteroidsToSpawn = Random.Range(3, 6);
        int smallAsteroidsToSpawn = Random.Range(3, 7);

        _asteroidSpawner.SpawnNewRound(largeAsteroidsToSpawn, Asteroid.Size.Large, _asteroids);
        _asteroidSpawner.SpawnNewRound(mediumAsteroidsToSpawn, Asteroid.Size.Medium, _asteroids);
        _asteroidSpawner.SpawnNewRound(smallAsteroidsToSpawn, Asteroid.Size.Small, _asteroids);
    }

    private void StartGame()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Game");
        }
    }
}
