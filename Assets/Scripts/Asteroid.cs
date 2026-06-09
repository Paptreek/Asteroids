using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float _moveSpeed;
    private Vector3 _size;
    private Vector3 _moveDirection;
    private Vector3 _spawnLocation;

    public bool HasBeenHit { get; private set; }
    public Size AsteroidSize { get; private set; }
    public enum Size { Large, Medium, Small }

    private void Start()
    {
        transform.position = _spawnLocation;
    }

    private void Update()
    {
        Move();
        ScreenManager.WrapAroundScreen(transform, 19.5f, 15.25f);
    }

    public void SetInitialSpawnData(Vector3 direction, Vector3 location)
    {
        _moveDirection = direction;
        _spawnLocation = location;

        Debug.Log($"X Direction: {direction.x}, Y Direction: {direction.y}");
    }

    public void SetSize(Size size)
    {
        if (size == Size.Large)
        {
            AsteroidSize = Size.Large;
            _size = new Vector3(4, 4, 4);
            _moveSpeed = 3;
        }

        if (size == Size.Medium)
        {
            AsteroidSize = Size.Medium;
            _size = new Vector3(2, 2, 2);
            _moveSpeed = 4;
        }

        if (size == Size.Small)
        {
            AsteroidSize = Size.Small;
            _size = new Vector3(1, 1, 1);
            _moveSpeed = 5;
        }

        transform.localScale = _size;
    }

    private void Move()
    {
        float moveSpeedX = _moveDirection.x * _moveSpeed * Time.deltaTime;
        float moveSpeedY = _moveDirection.y * _moveSpeed * Time.deltaTime;

        transform.Translate(moveSpeedX, moveSpeedY, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") || collision.CompareTag("Player"))
        {
            HasBeenHit = true;
        }
    }
}
