using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private Transform _coreTransform;
    [SerializeField] private Transform _cannonParentTransform;
    [SerializeField] private Sprite[] _expressionSprites = new Sprite[3];
    [SerializeField] private BossCannon[] _cannons = new BossCannon[8];
    [SerializeField] private GameObject _explosionEffectPrefab;

    private float _coreRotationTimer;
    private float _coreRotationSpeed;
    private float _expressionChangeTimer;
    private int _hitsTaken;
    private int _maxHP = 3;
    private BossMovement _movement;
    private SpriteRenderer _spriteRenderer;
    private List<BossCannon> _cannonTracker = new List<BossCannon>();

    private void Awake()
    {
        _movement = GetComponent<BossMovement>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        SetRandomRotationSpeed();

        foreach (BossCannon cannon in _cannons)
        {
            _cannonTracker.Add(cannon);
        }
    }

    private void Update()
    {
        _coreRotationTimer -= Time.deltaTime;
        _expressionChangeTimer -= Time.deltaTime;

        if (_coreTransform != null)
        {
            RotateCore();
            RemoveDestroyedCannons();
        }

        ChangeExpressionOnHit();

        if (_hitsTaken >= _maxHP)
        {
            Instantiate(_explosionEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag($"Player") || collision.CompareTag($"PlayerBullet"))
        {
            _hitsTaken++;
        }
    }

    private void SetRandomRotationSpeed()
    {
        float min = 25.0f;
        float max = 50.0f;

        _coreRotationSpeed = Random.value < 0.5f ? Random.Range(-max, -min) : Random.Range(min, max);
    }

    private void RotateCore()
    {
        _coreTransform.Rotate(0, 0, _coreRotationSpeed * Time.deltaTime);
        _cannonParentTransform.Rotate(0, 0, _coreRotationSpeed * Time.deltaTime);

        if (_coreRotationTimer <= 0)
        {
            SetRandomRotationSpeed();
            _coreRotationTimer = 5.0f;
        }
    }

    private void ChangeExpressionOnHit()
    {
        foreach (BossCannon cannon in _cannons)
        {
            if (cannon.WasJustHit)
            {
                _expressionChangeTimer = 1.0f;
                cannon.WasJustHit = false;
            }
        }

        if (_cannonTracker.Count <= 0)
        {
            _spriteRenderer.sprite = _expressionSprites[1];
        }
        else if (_expressionChangeTimer >= 0)
        {
            _spriteRenderer.sprite = _expressionSprites[2];
        }
        else
        {
            _spriteRenderer.sprite = _expressionSprites[0];
        }
    }

    private void RemoveDestroyedCannons()
    {
        foreach (BossCannon cannon in _cannonTracker.ToArray())
        {
            if (cannon.IsDamaged)
            {
                _cannonTracker.Remove(cannon);
            }
        }

        if (_cannonTracker.Count <= 0)
        {
            _movement.IncreaseSpeed();
            DestroyCore();
        }
    }

    private void DestroyCore()
    {
        foreach (BossCannon cannon in _cannons)
        {
            if (cannon != null)
            {
                Instantiate(_explosionEffectPrefab, cannon.transform.position, Quaternion.identity);
                Destroy(cannon.gameObject);
            }
        }

        Instantiate(_explosionEffectPrefab, _coreTransform.position, Quaternion.identity);
        Destroy(_coreTransform.gameObject);
    }
}
