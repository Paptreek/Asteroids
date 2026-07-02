using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TitleScreen
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _audioSettingsButton;
        [SerializeField] private Button _backButton;

        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;

        [SerializeField] private TMP_Text _highScoreText;

        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _audioSettings;

        private void Awake()
        {
            _playButton.onClick.AddListener(StartGame);
            _audioSettingsButton.onClick.AddListener(ToggleMusicSettings);
            _backButton.onClick.AddListener(ToggleMusicSettings);

            _musicSlider.value = 0.5f;
        }

        private void ToggleMusicSettings()
        {
            if (_mainMenu.activeInHierarchy)
            {
                _mainMenu.SetActive(false);
                _audioSettings.SetActive(true);
            }
            else
            {
                _mainMenu.SetActive(true);
                _audioSettings.SetActive(false);
            }
        }

        private void StartGame()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
