using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] AudioSettings _audioSettings;

    [SerializeField] private GameObject _mainMenuPanelObj;
    [SerializeField] private GameObject _audioMenuPanelObj;

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _audioButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _backButton;

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _effectsSlider;

    private void Awake()
    {
        _playButton.onClick.AddListener(TogglePauseMenu);
        _audioButton.onClick.AddListener(ToggleAudioMenu);
        _quitButton.onClick.AddListener(QuitGame);
        _backButton.onClick.AddListener(ToggleAudioMenu);
    }

    private void Start()
    {
        _musicSlider.value = _audioSettings.MusicVolume;
        _effectsSlider.value = _audioSettings.EffectsVolume;
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            AudioManager.Instance.StopPlayerShipMovement();
            TogglePauseMenu();
        }

        AdjustVolume();
    }

    private void TogglePauseMenu()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            _mainMenuPanelObj.SetActive(true);
            _audioMenuPanelObj.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            _mainMenuPanelObj.SetActive(false);
            _audioMenuPanelObj.SetActive(false);
        }
    }

    private void ToggleAudioMenu()
    {
        if (_mainMenuPanelObj.activeInHierarchy)
        {
            _mainMenuPanelObj.SetActive(false);
            _audioMenuPanelObj.SetActive(true);
        }
        else
        {
            _mainMenuPanelObj.SetActive(true);
            _audioMenuPanelObj.SetActive(false);
        }
    }

    private void AdjustVolume()
    {
        AudioManager.Instance.SetMusicVolume(_musicSlider);
        AudioManager.Instance.SetEffectsVolume(_effectsSlider);
    }

    private void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }
}
