using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] private AsteroidSpawner _asteroidSpawner;
    [SerializeField] private Asteroid _asteroid;

    private void Update()
    {
        SplitDestroyedAsteroids();
    }

    private void SplitDestroyedAsteroids()
    {
        foreach (Asteroid asteroid in _asteroidSpawner.Asteroids.ToArray())
        {
            if (asteroid.HasBeenShot)
            {
                if (asteroid.AsteroidSize == Asteroid.Size.Large)
                {
                    _asteroidSpawner.SpawnAsteroids(Asteroid.Size.Medium, asteroid.transform.position);
                }

                if (asteroid.AsteroidSize == Asteroid.Size.Medium)
                {
                    _asteroidSpawner.SpawnAsteroids(Asteroid.Size.Small, asteroid.transform.position);
                }

                _asteroidSpawner.Asteroids.Remove(asteroid);
                Destroy(asteroid.gameObject);
            }
        }
    }
}
