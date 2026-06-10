using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] private AsteroidSpawner _asteroidSpawner;
    [SerializeField] private Asteroid _asteroid;

    public List<Asteroid> Asteroids { get; private set; } = new List<Asteroid>();

    private void Update()
    {
        SplitDestroyedAsteroids();
    }

    private void SplitDestroyedAsteroids()
    {
        foreach (Asteroid asteroid in Asteroids.ToArray())
        {
            if (asteroid.HasBeenHit)
            {
                if (asteroid.AsteroidSize == Asteroid.Size.Large)
                {
                    _asteroidSpawner.SpawnFromDestroyed(Asteroid.Size.Medium, asteroid.transform.position, Asteroids);
                }

                if (asteroid.AsteroidSize == Asteroid.Size.Medium)
                {
                    _asteroidSpawner.SpawnFromDestroyed(Asteroid.Size.Small, asteroid.transform.position, Asteroids);
                }

                Asteroids.Remove(asteroid);
                Destroy(asteroid.gameObject);
            }
        }
    }
}
