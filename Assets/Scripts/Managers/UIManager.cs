using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _warpText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Image _powerUpIndicator;
    [SerializeField] private Sprite _multiAttackSprite;
    [SerializeField] private Sprite _shieldSprite;
    [SerializeField] private Sprite _pierceSprite;
    [SerializeField] private Sprite _emptySprite;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Player _player;
    [SerializeField] private PowerUpManager _powerUpManager;

    private void Update()
    {
        DisplayScore();
        DisplayWarpCount();
        DisplayCurrentPowerUp();
    }

    private void DisplayScore()
    {
        _scoreText.text = $"{_gameManager.Score:00000}";
    }

    private void DisplayWarpCount()
    {
        _warpText.text = $"WARPS: {_powerUpManager.WarpUses}";
    }

    private void DisplayCurrentPowerUp()
    {
        if (_powerUpManager.HasMultiShot)
        {
            _powerUpIndicator.sprite = _multiAttackSprite;
        }
        else if (_powerUpManager.HasShield)
        {
            _powerUpIndicator.sprite = _shieldSprite;
        }
        else if (_powerUpManager.HasPiercingAmmo)
        {
            _powerUpIndicator.sprite = _pierceSprite;
        }
        else
        {
            _powerUpIndicator.sprite = _emptySprite;
        }
    }
}
