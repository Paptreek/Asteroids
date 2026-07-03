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
        [SerializeField] private Slider _effectsSlider;

        [SerializeField] private TMP_Text _highScoreText;

        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _audioSettingsMenu;

        [SerializeField] private AudioSettings _audioSettings;

        private void Awake()
        {
            _playButton.onClick.AddListener(StartGame);
            _audioSettingsButton.onClick.AddListener(ToggleMusicSettingsMenu);
            _backButton.onClick.AddListener(ToggleMusicSettingsMenu);
        }

        private void Start()
        {
            _highScoreText.text = $"HI-SCORE: {PlayerPrefs.GetInt("HighScore"):00000}";

            _musicSlider.value = _audioSettings.MusicVolume;
            _effectsSlider.value = _audioSettings.EffectsVolume;
        }

        private void Update()
        {
            AdjustVolume();
        }

        private void ToggleMusicSettingsMenu()
        {
            if (_mainMenu.activeInHierarchy)
            {
                _mainMenu.SetActive(false);
                _audioSettingsMenu.SetActive(true);
            }
            else
            {
                _mainMenu.SetActive(true);
                _audioSettingsMenu.SetActive(false);
            }
        }

        private void StartGame()
        {
            SceneManager.LoadScene("Game");
        }

        private void AdjustVolume()
        {
            AudioManager.Instance.SetMusicVolume(_musicSlider);
            AudioManager.Instance.SetEffectsVolume(_effectsSlider);
        }
    }
}
