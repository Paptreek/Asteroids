using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _largeAsteroid;

    private List<GameObject> _largeAsteroids = new();

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject largeAsteroid = Instantiate(_largeAsteroid);
            _largeAsteroids.Add(largeAsteroid);
        }

        Debug.Log($"Number of Large Asteroids: {_largeAsteroids.Count}");
    }

    private void Update()
    {
        for (int i = 0; i < _largeAsteroids.Count; i++)
        {
            if (_largeAsteroids[i] == null)
            {
                Debug.Log($"Found a null asteroid. Removing from list.");
                _largeAsteroids.Remove(_largeAsteroids[i]);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            _largeAsteroids.Remove(gameObject);
            Destroy(gameObject);

            Debug.Log($"Number of Large Asteroids: {_largeAsteroids.Count}");
        }
    }
}
