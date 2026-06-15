using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] private AsteroidSpawner _asteroidSpawner;
    [SerializeField] private Asteroid _asteroid;
    [SerializeField] private AbilityManager _abilityManager;
    [SerializeField] private PowerUp _powerUp;

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
                if (asteroid.AsteroidSize == Asteroid.Size.Large && asteroid.DestroyedByPlayer)
                {
                    if (asteroid.DestroyedByPlayer)
                    {
                        LargeAsteroidsDestroyed++;
                    }

                    _asteroidSpawner.SpawnFromDestroyed(Asteroid.Size.Medium, asteroid.transform.position, Asteroids);
                }

                if (asteroid.AsteroidSize == Asteroid.Size.Medium)
                {
                    if (asteroid.DestroyedByPlayer)
                    {
                        MediumAsteroidsDestroyed++;
                    }

                    _asteroidSpawner.SpawnFromDestroyed(Asteroid.Size.Small, asteroid.transform.position, Asteroids);
                }

                if (asteroid.AsteroidSize == Asteroid.Size.Small)
                {
                    if (asteroid.DestroyedByPlayer)
                    {
                        SmallAsteroidsDestroyed++;
                    }
                }

                MaybeDropPowerUp(asteroid);

                Asteroids.Remove(asteroid);
                Destroy(asteroid.gameObject);
            }
        }
    }

    private void MaybeDropPowerUp(Asteroid asteroid)
    {
        int randomNumber = Random.Range(0, 16);

        if (randomNumber == 15)
        {
            PowerUp powerUp = Instantiate(_powerUp, asteroid.transform.position, Quaternion.identity);
            powerUp.SetAbilityManager(_abilityManager);
        }
    }
}
