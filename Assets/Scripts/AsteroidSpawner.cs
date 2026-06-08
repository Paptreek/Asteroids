using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private Asteroid _asteroid;

    private List<Asteroid> _asteroids = new();

    private void Start()
    {
        SpawnNewAsteroids(4, Asteroid.Size.Large);
    }

    private void Update()
    {
        RemoveDestroyedAsteroids();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            _asteroids.Remove(_asteroid);
            Destroy(gameObject);

            Debug.Log($"Number of Large Asteroids: {_asteroids.Count}");
        }
    }

    private void SpawnNewAsteroids(int numberToSpawn, Asteroid.Size size)
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            float directionX = Random.value > 0.5f ? Random.Range(-1.0f, -0.25f) : Random.Range(0.25f, 1.0f);
            float directionY = Random.value > 0.5f ? Random.Range(-1.0f, -0.25f) : Random.Range(0.25f, 1.0f);
            float locationX = Random.value > 0.5f ? Random.Range(-15.0f, -5.0f) : Random.Range(5.0f, 15.0f);
            float locationY = Random.value > 0.5f ? Random.Range(-15.0f, -5.0f) : Random.Range(5.0f, 15.0f);

            Asteroid asteroid = Instantiate(_asteroid);
            asteroid.SetSize(size);
            asteroid.SetInitialSpawnData(directionX, directionY, locationX, locationY);
            _asteroids.Add(asteroid);
        }
    }

    private void RemoveDestroyedAsteroids()
    {
        for (int i = 0; i < _asteroids.Count; i++)
        {
            if (_asteroids[i] == null)
            {
                Debug.Log($"Found a null asteroid. Removing from list.");
                _asteroids.Remove(_asteroids[i]);
            }
        }
    }
}
