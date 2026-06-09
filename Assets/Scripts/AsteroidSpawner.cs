using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private Asteroid _asteroid;

    public List<Asteroid> Asteroids { get; private set; } = new();

    private void Start()
    {
        SpawnNewRound(4, Asteroid.Size.Large); // this call should move to GameManager eventually
    }

    public void SpawnAsteroids(Asteroid.Size size, Vector3 location)
    {
        for (int i = 0; i < 2; i++)
        {
            float directionX = Random.value > 0.5f ? Random.Range(-1.0f, -0.25f) : Random.Range(0.25f, 1.0f);
            float directionY = Random.value > 0.5f ? Random.Range(-1.0f, -0.25f) : Random.Range(0.25f, 1.0f);
            Vector3 direction = new Vector3(directionX, directionY);

            Asteroid asteroid = Instantiate(_asteroid);
            asteroid.SetSize(size);
            asteroid.SetInitialSpawnData(direction, location);
            Asteroids.Add(asteroid);
        }
    }

    private void SpawnNewRound(int numberToSpawn, Asteroid.Size size)
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            float directionX = Random.value > 0.5f ? Random.Range(-1.0f, -0.25f) : Random.Range(0.25f, 1.0f);
            float directionY = Random.value > 0.5f ? Random.Range(-1.0f, -0.25f) : Random.Range(0.25f, 1.0f);
            Vector3 direction = new Vector3(directionX, directionY);

            float locationX = Random.value > 0.5f ? Random.Range(-15.0f, -5.0f) : Random.Range(5.0f, 15.0f);
            float locationY = Random.value > 0.5f ? Random.Range(-15.0f, -5.0f) : Random.Range(5.0f, 15.0f);
            Vector3 location = new Vector3(locationX, locationY);

            Asteroid asteroid = Instantiate(_asteroid);
            asteroid.SetSize(size);
            asteroid.SetInitialSpawnData(direction, location);
            Asteroids.Add(asteroid);
        }
    }
}
