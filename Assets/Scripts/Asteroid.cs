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
    private Vector3 _scale; // maybe turn this into an enum { Small, Medium, Large }

    private void Awake()
    {
        float moveDirectionX = Random.Range(-1.0f, 1.0f);
        float moveDirectionY = Random.Range(-1.0f, 1.0f);
        _moveDirection = new Vector3(moveDirectionX, moveDirectionY);

        // testing only. don't actually need random sizes, everything should spawn as a Large and split into smaller ones as they get destroyed
        float scale = Random.Range(1.0f, 5.0f);
        _scale = new Vector3(scale, scale);
    }

    private void Start()
    {
        transform.localScale = _scale;
    }

    private void Update()
    {
        MoveInRandomDirection();
        ScreenManager.WrapAroundScreen(transform);
    }

    private void MoveInRandomDirection()
    {
        float moveSpeedX = _moveDirection.x * _moveSpeed * Time.deltaTime;
        float moveSpeedY = _moveDirection.y * _moveSpeed * Time.deltaTime;

        transform.Translate(moveSpeedX, moveSpeedY, 0);
    }
}
