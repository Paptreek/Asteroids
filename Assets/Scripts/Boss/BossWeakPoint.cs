using UnityEngine;

public class BossWeakPoint : MonoBehaviour
{
    private int _numberOfTimesHit;
    private Vector2 _topSide = new Vector2(0, 0.5f);
    private Vector2 _rightSide = new Vector2(0.5f, 0);
    private Vector2 _bottomSide = new Vector2(0, -0.5f);
    private Vector2 _leftSide = new Vector2(-0.5f, 0);

    private void Start()
    {
        SwitchPosition();
    }

    private void Update()
    {
        if (_numberOfTimesHit >= 5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag($"Player") || collision.CompareTag($"PlayerBullet"))
        {
            _numberOfTimesHit++;
            Debug.Log($"WeakPoint Hit {_numberOfTimesHit} times.");

            SwitchPosition();
        }
    }

    private void SwitchPosition()
    {
        int random = Random.Range(0, 4);

        Vector2 position = random switch
        {
            0 => _topSide,
            1 => _rightSide,
            2 => _bottomSide,
            3 => _leftSide,
            _ => _topSide
        };

        transform.localPosition = position;

        Debug.Log($"Switched position to: {position}");
    }
}
