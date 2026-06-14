using System.Collections.Generic;
using UnityEngine;

public class RespawnArea : MonoBehaviour
{
    private List<Collider2D> _colliders = new List<Collider2D>();

    public bool IsClearOfDanger { get; private set; } = true;

    private void Update()
    {
        CheckIfEmpty();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid") || collision.CompareTag("EnemyShip") || collision.CompareTag("EnemyBullet"))
        {
            IsClearOfDanger = false;
        }
    }

    private void CheckIfEmpty()
    {
        GetComponent<BoxCollider2D>().Overlap(_colliders);

        if (_colliders.Count == 0)
        {
            IsClearOfDanger = true;
        }
    }
}
