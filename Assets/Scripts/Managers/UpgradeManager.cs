using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PowerUpManager _abilityManager;
    [SerializeField] private PlayerShipCannon _playerShipCannon;

    [SerializeField] private GameObject _upgradePanel;
    [SerializeField] private GameObject _upgradePanelBackground;

    [SerializeField] private GameObject _attackSpeedPanel;
    [SerializeField] private GameObject _bulletSpeedPanel;
    [SerializeField] private GameObject _thrustersPanel;
    [SerializeField] private GameObject _turnSpeedPanel;
    [SerializeField] private GameObject _warpUsesPanel;

    [SerializeField] private Button _selectAttackSpeedButton;
    [SerializeField] private Button _selectBulletSpeedButton;
    [SerializeField] private Button _selectThrustersButton;
    [SerializeField] private Button _selectTurnSpeedButton;
    [SerializeField] private Button _selectWarpUsesButton;

    private GameObject _leftPanel;
    private GameObject _centerPanel;
    private GameObject _rightPanel;

    public List<GameObject> UpgradeOptions { get; private set; } = new List<GameObject>();

    private void Awake()
    {
        UpgradeOptions.Add(_attackSpeedPanel);
        UpgradeOptions.Add(_bulletSpeedPanel);
        UpgradeOptions.Add(_thrustersPanel);
        UpgradeOptions.Add(_turnSpeedPanel);
        UpgradeOptions.Add(_warpUsesPanel);
    }

    private void Start()
    {
        _selectAttackSpeedButton.onClick.AddListener(UpgradeAttackSpeed);
        _selectBulletSpeedButton.onClick.AddListener(UpgradeBulletSpeed);
        _selectThrustersButton.onClick.AddListener(UpgradeThrusters);
        _selectTurnSpeedButton.onClick.AddListener(UpgradeTurnSpeed);
        _selectWarpUsesButton.onClick.AddListener(UpgradeWarpUses);
    }

    public void ShuffleUpgrades<T>(List<T> array)
    {
        for (int i = UpgradeOptions.Count - 1; i > 1; i--)
        {
            int random = Random.Range(0, i);
            T temp = array[i];
            array[i] = array[random];
            array[random] = temp;
        }
    }

    public void AssignRandomUpgrades()
    {
        _leftPanel = UpgradeOptions[0];
        _centerPanel = UpgradeOptions[1];
        _rightPanel = UpgradeOptions[2];

        _leftPanel.GetComponentInChildren<Button>().Select();
    }

    public void DisplayUpgrades()
    {
        float leftPositionX = -202.5f;
        float centerPositionX = 0.0f;
        float rightPositionX = 202.5f;
        float postionY = -27.2f;

        _leftPanel.transform.localPosition = new Vector3(leftPositionX, postionY);
        _centerPanel.transform.localPosition = new Vector3(centerPositionX, postionY);
        _rightPanel.transform.localPosition = new Vector3(rightPositionX, postionY);
        
        _leftPanel.SetActive(true);
        _centerPanel.SetActive(true);
        _rightPanel.SetActive(true);
    }

    private void UpgradeAttackSpeed()
    {
        Debug.Log($"Attack Speed Upgraded!");
        _playerShipCannon.DecreaseTimeBetweenAttacks(0.075f);
        HidePanelsAndResume();
    }

    private void UpgradeBulletSpeed()
    {
        Debug.Log($"Bullet Speed Upgraded!");
        _playerShipCannon.BulletSpeedUpgradeAmount += 5;
        HidePanelsAndResume();
    }

    private void UpgradeThrusters()
    {
        Debug.Log($"Thrusters Upgraded!");
        _playerMovement.IncreaseMoveSpeed(100f);
        HidePanelsAndResume();
    }

    private void UpgradeTurnSpeed()
    {
        Debug.Log($"Turn Speed Upgraded!");
        _playerMovement.IncreaseTurnSpeed(25f);
        HidePanelsAndResume();
    }

    private void UpgradeWarpUses()
    {
        Debug.Log($"Warp Uses Upgraded!");
        _abilityManager.IncreaseMaxWarpUses();
        HidePanelsAndResume();
    }

    private void HidePanelsAndResume()
    {
        _upgradePanelBackground.SetActive(false);
        _upgradePanel.SetActive(false);

        _leftPanel.SetActive(false);
        _centerPanel.SetActive(false);
        _rightPanel.SetActive(false);

        Time.timeScale = 1;

        _leftPanel.GetComponentInChildren<Button>().Select();
    }
}
