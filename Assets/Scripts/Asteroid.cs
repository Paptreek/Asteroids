using UnityEngine;

/// <summary>
///  
/// Potential solution for Asteroid management:
///  
/// 1. Initially spawn some number of Large Asteroids. Like 4 or 5, idk dude I'm not a genius.
/// 2. When a Large Asteroid is shot, it should be destroyed. From that location, 2 Medium Asteroids should spawn.
/// 3. When a Medium Asteroid is shot, destroy. 2 Small Asteroids should spawn.
/// 4. Large, Medium, and Small Asteroids all have different speeds and point values.
/// 5. When an Asteroid is destroyed, the newly spawned ones should get new movement directions.
/// 
/// </summary>

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 4;

    private Vector3 _moveDirection;
    private Vector3 _spawnLocation;

    private void Awake()
    {
        float moveDirectionX = Random.value > 0.5f ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f);
        float moveDirectionY = Random.value > 0.5f ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f);
        _moveDirection = new Vector3(moveDirectionX, moveDirectionY);

        float spawnLocationX = Random.value > 0.5f ? Random.Range(-15.0f, -5.0f) : Random.Range(5.0f, 15.0f);
        float spawnLocationY = Random.value > 0.5f ? Random.Range(-15.0f, -5.0f) : Random.Range(5.0f, 15.0f);
        _spawnLocation = new Vector3(spawnLocationX, spawnLocationY);

        Debug.Log($"X Direction: {moveDirectionX}, Y Direction: {moveDirectionY}");
    }

    private void Start()
    {
        transform.position = _spawnLocation;
    }

    private void Update()
    {
        MoveInRandomDirection();
        ScreenManager.WrapAroundScreen(transform, 20.0f, 16.0f);
    }

    private void MoveInRandomDirection()
    {
        float moveSpeedX = _moveDirection.x * _moveSpeed * Time.deltaTime;
        float moveSpeedY = _moveDirection.y * _moveSpeed * Time.deltaTime;

        transform.Translate(moveSpeedX, moveSpeedY, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
