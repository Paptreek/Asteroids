using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] private AsteroidSpawner _asteroidSpawner;
    [SerializeField] private Asteroid _asteroid;

    public int LargeAsteroidsDestroyed { get; private set; }
    public int MediumAsteroidsDestroyed { get; private set; }
    public int SmallAsteroidsDestroyed { get; private set; }
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
                    LargeAsteroidsDestroyed++;
                    _asteroidSpawner.SpawnFromDestroyed(Asteroid.Size.Medium, asteroid.transform.position, Asteroids);
                }

                if (asteroid.AsteroidSize == Asteroid.Size.Medium)
                {
                    MediumAsteroidsDestroyed++;
                    _asteroidSpawner.SpawnFromDestroyed(Asteroid.Size.Small, asteroid.transform.position, Asteroids);
                }

                if (asteroid.AsteroidSize == Asteroid.Size.Small)
                {
                    SmallAsteroidsDestroyed++;
                }

                Debug.Log($"Asteroids Destroyed: LG: {LargeAsteroidsDestroyed}, MD: {MediumAsteroidsDestroyed}, SM: {SmallAsteroidsDestroyed}");

                Asteroids.Remove(asteroid);
                Destroy(asteroid.gameObject);
            }
        }
    }
}
