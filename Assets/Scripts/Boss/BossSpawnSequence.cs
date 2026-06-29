using UnityEngine;

public class BossSpawnSequence : MonoBehaviour
{
    [SerializeField] private Transform[] _cornerCannonTransforms = new Transform[4];
    [SerializeField] private Transform[] _centerCannonTransforms = new Transform[4];

    private BossMovement _movement;
    private BossCannonManager _canonManager;
    private float _endSpawnSequenceTimer = 3.0f;

    // ending local positions for the cannon peices
    private float _moveSpeed = 2.5f;
    private Vector3 _endTopLeft = new Vector3(-0.9062f, 0.9062f, 0);
    private Vector3 _endBottomLeft = new Vector3(-0.9062f, -0.9062f, 0);
    private Vector3 _endBottomRight = new Vector3(0.9062f, -0.9062f, 0);
    private Vector3 _endTopRight = new Vector3(0.9062f, 0.9062f, 0);
    private Vector3 _endTop = new Vector3(0, 1.5315f, 0);
    private Vector3 _endLeft = new Vector3(-1.5315f, 0, 0);
    private Vector3 _endBottom = new Vector3(0, -1.5315f, 0);
    private Vector3 _endRight = new Vector3(1.5315f, 0, 0);

    public bool SpawnSequenceComplete { get; private set; } 
    
    private void Awake()
    {
        _movement = GetComponent<BossMovement>();
        _canonManager = GetComponent<BossCannonManager>();
        
        SetInitialPositions();
    }

    private void Update()
    {
        if (!SpawnSequenceComplete)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 6.5f, 0), 5.0f * Time.deltaTime);

            if (transform.position.y == 6.5f)
            {
                AttachCannons();
                _endSpawnSequenceTimer -= Time.deltaTime;
            }
        }

        if (_endSpawnSequenceTimer <= 0)
        {
            EnableAllScripts();
            SpawnSequenceComplete = true;
            enabled = false;
        }
    }

    private void AttachCannons()
    {
        Vector3[] endPositions = { _endTopLeft, _endBottomLeft, _endBottomRight, _endTopRight };

        for (int i = 0; i < 4; i++)
        {
            _cornerCannonTransforms[i].localPosition = Vector3.MoveTowards(_cornerCannonTransforms[i].localPosition, endPositions[i], _moveSpeed * Time.deltaTime);
        }

        Vector3[] centerEndPositions = { _endTop, _endLeft, _endBottom, _endRight };

        for (int i = 0; i < 4; i++)
        {
            _centerCannonTransforms[i].localPosition = Vector3.MoveTowards(_centerCannonTransforms[i].localPosition, centerEndPositions[i], _moveSpeed * Time.deltaTime);
        }
    }

    private void EnableAllScripts()
    {
        _movement.enabled = true;
        _canonManager.enabled = true;
    }

    private void SetInitialPositions()
    {
        transform.position = new Vector3(0, -6.5f, 0);

        float min = 1.0f;
        float max = 3.0f;

        foreach (Transform transform in _cornerCannonTransforms)
        {
            float randomX = Random.value < 0.5f ? Random.Range(-max, -min) : Random.Range(min, max);
            float randomY = Random.value < 0.5f ? Random.Range(-max, -min) : Random.Range(min, max);

            transform.localPosition = new Vector3(randomX, randomY);
        }

        foreach (Transform transform in _centerCannonTransforms)
        {
            float randomX = Random.value < 0.5f ? Random.Range(-max, -min) : Random.Range(min, max);
            float randomY = Random.value < 0.5f ? Random.Range(-max, -min) : Random.Range(min, max);

            transform.localPosition = new Vector3(randomX, randomY);
        }
    }
}
