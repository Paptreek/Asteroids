using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private BossWeakPoint _weakPoint;
    [SerializeField] private Sprite[] _expressionSprites = new Sprite[3];
    [SerializeField] private BossCannon[] _cannons = new BossCannon[8];

    private float _expressionChangeTimer;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _expressionChangeTimer -= Time.deltaTime;

        if (_weakPoint == null)
        {
            Debug.Log($"Boss detects WeakPoint is null");
        }

        ChangeExpressionOnHit();
        FlashAngryExpression();
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
    }

    private void FlashAngryExpression()
    {
        if (_expressionChangeTimer >= 0)
        {
            _spriteRenderer.sprite = _expressionSprites[2];
        }
        else
        {
            _spriteRenderer.sprite = _expressionSprites[0];
        }
    }
}
