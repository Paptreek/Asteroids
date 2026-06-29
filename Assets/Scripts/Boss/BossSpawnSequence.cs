using UnityEngine;

public class BossSpawnSequence : MonoBehaviour
{
    [SerializeField] private Transform[] _cornerCannonTransforms = new Transform[4];
    [SerializeField] private Transform[] _centerCannonTransforms = new Transform[4];

    private BossMovement _movement;
    private BossCannonManager _canonManager;
    private float _endSpawnSequenceTimer = 3.0f;

    // ending local positions for the cannon peices
    
    private float _attachSpeedCorners = 4.0f;
    private float _attachSpeedCenters = 4.0f;
    
    private Vector3[] _endPositionsCorners;
    private Vector3 _endTopLeft = new Vector3(-0.9062f, 0.9062f, 0);
    private Vector3 _endBottomLeft = new Vector3(-0.9062f, -0.9062f, 0);
    private Vector3 _endBottomRight = new Vector3(0.9062f, -0.9062f, 0);
    private Vector3 _endTopRight = new Vector3(0.9062f, 0.9062f, 0);

    private Vector3[] _endPositionsCenters;
    private Vector3 _endTop = new Vector3(0, 1.5315f, 0);
    private Vector3 _endLeft = new Vector3(-1.5315f, 0, 0);
    private Vector3 _endBottom = new Vector3(0, -1.5315f, 0);
    private Vector3 _endRight = new Vector3(1.5315f, 0, 0);

    public bool SpawnSequenceComplete { get; private set; }

    private void Awake()
    {
        _movement = GetComponent<BossMovement>();
        _canonManager = GetComponent<BossCannonManager>();

        _endPositionsCorners = new Vector3[] { _endTopLeft, _endBottomLeft, _endBottomRight, _endTopRight };
        _endPositionsCenters = new Vector3[] { _endTop, _endLeft, _endBottom, _endRight };
    }

    private void Update()
    {
        AttachCannons();

        if (_cornerCannonTransforms[0].localPosition == _endTopLeft && _centerCannonTransforms[0].localPosition == _endTop)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 6.5f, 0), 5.0f * Time.deltaTime);
            _endSpawnSequenceTimer -= Time.deltaTime;
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
        for (int i = 0; i < 4; i++)
        {
            Vector3 localPosition = _cornerCannonTransforms[i].localPosition;

            _cornerCannonTransforms[i].gameObject.SetActive(true);
            _cornerCannonTransforms[i].localPosition = Vector3.MoveTowards(localPosition, _endPositionsCorners[i], _attachSpeedCorners * Time.deltaTime);
        }

        Vector3[] centerEndPositions = { _endTop, _endLeft, _endBottom, _endRight };

        for (int i = 0; i < 4; i++)
        {
            Vector3 localPosition = _centerCannonTransforms[i].localPosition;

            _centerCannonTransforms[i].gameObject.SetActive(true);
            _centerCannonTransforms[i].localPosition = Vector3.MoveTowards(localPosition, _endPositionsCenters[i], _attachSpeedCenters * Time.deltaTime);
        }
    }

    private void EnableAllScripts()
    {
        _movement.enabled = true;
        _canonManager.enabled = true;
    }
}
