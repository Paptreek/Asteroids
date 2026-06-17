using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private Asteroid _asteroid;
    [SerializeField] private AsteroidManager _asteroidManager;

    public void SpawnFromDestroyed(Asteroid.Size size, Vector3 location, List<Asteroid> asteroidList)
    {
        for (int i = 0; i < 2; i++)
        {
            float directionX = Random.value > 0.5f ? Random.Range(-1.0f, -0.25f) : Random.Range(0.25f, 1.0f);
            float directionY = Random.value > 0.5f ? Random.Range(-1.0f, -0.25f) : Random.Range(0.25f, 1.0f);
            Vector3 direction = new(directionX, directionY);

            float randomLocationX = Random.Range(-1.0f, 1.0f);
            float randomLocationY = Random.Range(-1.0f, 1.0f);

            Asteroid asteroid = Instantiate(_asteroid);
            asteroid.SetAsteroidManager(_asteroidManager);
            asteroid.SetSize(size);
            asteroid.SetInitialSpawnData(direction, new Vector3(location.x + randomLocationX, location.y + randomLocationY));
            asteroidList.Add(asteroid);
        }
    }

    public void SpawnNewRound(int numberToSpawn, Asteroid.Size size, List<Asteroid> asteroidList)
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            float directionX = Random.value > 0.5f ? Random.Range(-1.0f, -0.25f) : Random.Range(0.25f, 1.0f);
            float directionY = Random.value > 0.5f ? Random.Range(-1.0f, -0.25f) : Random.Range(0.25f, 1.0f);
            Vector3 direction = new(directionX, directionY);

            float locationX = Random.value > 0.5f ? Random.Range(-15.0f, -5.0f) : Random.Range(5.0f, 15.0f);
            float locationY = Random.value > 0.5f ? Random.Range(-15.0f, -5.0f) : Random.Range(5.0f, 15.0f);
            Vector3 location = new(locationX, locationY);

            Asteroid asteroid = Instantiate(_asteroid);
            asteroid.SetAsteroidManager(_asteroidManager);
            asteroid.SetSize(size); // this isn't really necessary, but useful for testing
            asteroid.SetInitialSpawnData(direction, location);
            asteroidList.Add(asteroid);
        }

        //Debug.Log($"Number of asteroids spawned: {numberToSpawn}");
    }
}
