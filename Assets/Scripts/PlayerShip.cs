using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    private void Update()
    {
        ScreenManager.WrapAroundScreen(transform, 17.5f, 13.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
