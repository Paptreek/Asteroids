using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    [SerializeField] private GameObject _upgradePanel;

    [SerializeField] private GameObject _attackSpeedPanel;
    [SerializeField] private GameObject _bulletSpeedPanel;
    [SerializeField] private GameObject _thrustersPanel;
    [SerializeField] private GameObject _turnSpeedPanel;
    [SerializeField] private GameObject _warpUsesPanel;

    [SerializeField] private Button _selectThrustersButton;
    [SerializeField] private Button _selectTurnSpeedButton;
    [SerializeField] private Button _selectBulletSpeedButton;

    private UpgradeOption _upgradeOption;

    public enum UpgradeOption { AttackSpeed, BulletSpeed, Thrusters, TurnSpeed, WarpUses }

    private void Start()
    {
        DisplayRandomUpgrades();

        _selectThrustersButton.onClick.AddListener(UpgradeThrusters);
        _selectTurnSpeedButton.onClick.AddListener(UpgradeTurnSpeed);
        _selectBulletSpeedButton.onClick.AddListener(UpgradeBulletSpeed);
    }

    private void DisplayRandomUpgrades()
    {
        GameObject leftPanel = _attackSpeedPanel;
        GameObject centerPanel = _thrustersPanel;
        GameObject rightPanel = _turnSpeedPanel;

        float leftPositionX = -202.5f;
        float centerPositionX = 0.0f;
        float rightPositionX = 202.5f;
        float postionY = -27.2f;

        leftPanel.transform.localPosition = new Vector3(leftPositionX, postionY);
        centerPanel.transform.localPosition = new Vector3(centerPositionX, postionY);
        rightPanel.transform.localPosition = new Vector3(rightPositionX, postionY);

        leftPanel.SetActive(true);
        centerPanel.SetActive(true);
        rightPanel.SetActive(true);
    }

    private void UpgradeThrusters()
    {
        Debug.Log($"Thrusters Upgraded!");
        _playerMovement.IncreaseMoveSpeed(250f); // testing
        _upgradePanel.SetActive(false);
        Time.timeScale = 1;
    }

    private void UpgradeTurnSpeed()
    {
        Debug.Log($"Turn Speed Upgraded!");
        _playerMovement.IncreaseTurnSpeed(50f); // testing
        _upgradePanel.SetActive(false);
        Time.timeScale = 1;
    }

    private void UpgradeBulletSpeed()
    {
        Debug.Log($"Bullet Speed Upgraded!");

    }
}
