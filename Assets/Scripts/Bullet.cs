using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _destroyTimer = 0.75f;
    private float _travelSpeed = 25;

    private void Update()
    {
        _destroyTimer -= Time.deltaTime;

        transform.Translate(new Vector2(0, _travelSpeed * Time.deltaTime));

        ScreenManager.WrapAroundScreen(transform);
        DestroyAfterCountdown();
    }

    private void DestroyAfterCountdown()
    {
        if (_destroyTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
