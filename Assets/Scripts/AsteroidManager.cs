using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] private AsteroidSpawner _asteroidSpawner;
    [SerializeField] private Asteroid _asteroid;

    private void Update()
    {
        foreach (Asteroid asteroid in _asteroidSpawner.Asteroids.ToArray())
        {
            if (asteroid != null && asteroid.HasBeenShot)
            {
                if (asteroid.AsteroidSize == Asteroid.Size.Large)
                {
                    _asteroidSpawner.SpawnAsteroids(Asteroid.Size.Medium, asteroid.transform.position);
                    asteroid.HasBeenShot = false;
                    _asteroidSpawner.Asteroids.Remove(asteroid);
                    Destroy(asteroid.gameObject);
                }

                if (asteroid.AsteroidSize == Asteroid.Size.Medium)
                {
                    _asteroidSpawner.SpawnAsteroids(Asteroid.Size.Small, asteroid.transform.position);
                    asteroid.HasBeenShot = false;
                    _asteroidSpawner.Asteroids.Remove(asteroid);
                    Destroy(asteroid.gameObject);
                }

                if (asteroid.AsteroidSize == Asteroid.Size.Small)
                {
                    asteroid.HasBeenShot = false;
                    _asteroidSpawner.Asteroids.Remove(asteroid);
                    Destroy(asteroid.gameObject);
                }

                Debug.Log($"AsteroidManager just detected a destroyed asteroid. Size: {asteroid.AsteroidSize}");
            }
        }
    }
}
