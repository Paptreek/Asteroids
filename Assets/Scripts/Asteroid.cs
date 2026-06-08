using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float _moveSpeed;
    private Vector3 _size;
    private Vector3 _moveDirection;
    private Vector3 _spawnLocation;

    public enum Size { Large, Medium, Small }

    private void Awake()
    {
        //SetInitialSpawnData();
    }

    private void Start()
    {
        transform.position = _spawnLocation;
    }

    private void Update()
    {
        Move();
        ScreenManager.WrapAroundScreen(transform, 19.5f, 15.25f);
    }

    public void SetInitialSpawnData(float directionX, float directionY, float locationX, float locationY)
    {
        _moveDirection = new Vector3(directionX, directionY);
        _spawnLocation = new Vector3(locationX, locationY);

        Debug.Log($"X Direction: {directionX}, Y Direction: {directionY}");
    }

    public void SetSize(Size size)
    {
        if (size == Size.Large)
        {
            _size = new Vector3(4, 4, 4);
            _moveSpeed = 3;
        }

        if (size == Size.Medium)
        {
            _size = new Vector3(2, 2, 2);
            _moveSpeed = 4;
        }

        if (size == Size.Small)
        {
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
        if (collision.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
