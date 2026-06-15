using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private Sprite _multiShotSprite;
    [SerializeField] private Sprite _shieldSprite;
    [SerializeField] private Sprite _pierceSprite;

    private SpriteRenderer _spriteRenderer;
    private AbilityManager _abilityManager;
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
        Destroy(gameObject, 5.0f);
    }

    public void SetAbilityManager(AbilityManager abilityManager)
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
        int random = Random.Range(1, 3);

        Type powerUpType = random switch
        {
            1 => Type.MultiShot,
            2 => Type.Shield,
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
    }

    private void AddPowerUpToPlayer()
    {
        if (_type == Type.MultiShot)
        {
            _abilityManager.HasMultiShot = true;
            _abilityManager.HasShield = false;
        }
        else if (_type == Type.Shield)
        {
            _abilityManager.HasShield = true;
            _abilityManager.HasMultiShot = false;
        }
    }
}
