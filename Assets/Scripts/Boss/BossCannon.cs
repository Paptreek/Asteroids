using UnityEngine;

public class BossCannon : MonoBehaviour
{
    [SerializeField] private Sprite _damagedCannonSprite;
    [SerializeField] private GameObject _explosionEffectPrefab;

    private int _numberOfHitsTaken;
    private SpriteRenderer _spriteRenderer;

    public bool WasJustHit { get; set; }
    public bool IsDamaged { get; private set; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_numberOfHitsTaken >= 3 && _spriteRenderer.sprite != _damagedCannonSprite)
        {
            if (_damagedCannonSprite != null)
            {
                _spriteRenderer.sprite = _damagedCannonSprite;
                IsDamaged = true;
            }
            else
            {
                Instantiate(_explosionEffectPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag($"PlayerBullet") || collision.CompareTag($"Player"))
        {
            WasJustHit = true;
            _numberOfHitsTaken++;
            Debug.Log($"Cannon hits taken: {_numberOfHitsTaken}");
        }
    }
}
