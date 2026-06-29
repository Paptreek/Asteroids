using UnityEngine;

public class BossCannon : MonoBehaviour
{
    [SerializeField] private Sprite _damagedCannonSprite;
    [SerializeField] private GameObject _explosionEffectPrefab;

    private int _hitsTaken;
    private int _maxHP = 3;
    private SpriteRenderer _spriteRenderer;

    public bool WasJustHit { get; set; }
    public bool IsDamaged { get; private set; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_hitsTaken >= _maxHP && _spriteRenderer.sprite != _damagedCannonSprite)
        {
            GetComponent<PolygonCollider2D>().enabled = false;
            IsDamaged = true;
            
            if (_damagedCannonSprite != null)
            {
                _spriteRenderer.sprite = _damagedCannonSprite;
                // add explosion sound
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
            _hitsTaken++;
            Debug.Log($"Cannon hits taken: {_hitsTaken}");
        }
    }
}
