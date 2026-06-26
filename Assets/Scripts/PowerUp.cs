using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private Sprite _multiShotSprite;
    [SerializeField] private Sprite _shieldSprite;
    [SerializeField] private Sprite _pierceSprite;

    private float _enableFlashTimer = 5.0f;
    private float _spriteRendererFlashTimer = 0.5f;
    private SpriteRenderer _spriteRenderer;
    private PowerUpManager _abilityManager;
    private Type _type;

    public enum Type { MultiShot, Shield, PiercingAmmo }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        _enableFlashTimer -= Time.deltaTime;
        _spriteRendererFlashTimer -= Time.deltaTime;

        if (_enableFlashTimer <= 2.5f)
        {
            FlashSpriteRenderer();
        }

        Destroy(gameObject, 5.0f);
    }

    public void SetAbilityManager(PowerUpManager abilityManager)
    {
        _abilityManager = abilityManager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AddPowerUpToPlayer();
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        int random = Random.Range(1, 4);

        Type powerUpType = random switch
        {
            1 => Type.MultiShot,
            2 => Type.Shield,
            3 => Type.PiercingAmmo,
            _ => Type.MultiShot
        };

        _type = powerUpType;

        if (_type == Type.MultiShot)
        {
            _spriteRenderer.sprite = _multiShotSprite;
        }
        else if (_type == Type.Shield)
        {
            _spriteRenderer.sprite = _shieldSprite;
        }
        else if (_type == Type.PiercingAmmo)
        {
            _spriteRenderer.sprite = _pierceSprite;
        }
    }

    private void AddPowerUpToPlayer()
    {
        _abilityManager.ClearPowerUps();

        if (_type == Type.MultiShot)
        {
            _abilityManager.HasMultiShot = true;
        }
        else if (_type == Type.Shield)
        {
            _abilityManager.HasShield = true;
        }
        else if (_type == Type.PiercingAmmo)
        {
            _abilityManager.HasPiercingAmmo = true;
        }
    }

    private void FlashSpriteRenderer()
    {
        if (_spriteRenderer.enabled && _spriteRendererFlashTimer <= 0)
        {
            _spriteRenderer.enabled = false;
            _spriteRendererFlashTimer = 0.25f;
        }
        else if (!_spriteRenderer.enabled && _spriteRendererFlashTimer <= 0)
        {
            _spriteRenderer.enabled = true;
            _spriteRendererFlashTimer = 0.25f;
        }
    }
}
