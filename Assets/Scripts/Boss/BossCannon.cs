using UnityEngine;

public class BossCannon : MonoBehaviour
{
    private int _numberOfHitsTaken;

    private void Update()
    {
        if (_numberOfHitsTaken >= 3)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag($"PlayerBullet") || collision.CompareTag($"Player"))
        {
            _numberOfHitsTaken++;
            Debug.Log($"Cannon hits taken: {_numberOfHitsTaken}");
        }
    }
}
