using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioSettings _audioSettings;

    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _ambience;
    [SerializeField] private AudioSource _playerShoot;
    [SerializeField] private AudioSource _playerWarp;
    [SerializeField] private AudioSource _playerDeath;
    [SerializeField] private AudioSource _playerShipMovement;
    [SerializeField] private AudioSource _enemyShoot;
    [SerializeField] private AudioSource _enemyDamaged;
    [SerializeField] private AudioSource _enemyShipMovement;
    [SerializeField] private AudioSource _powerUpPickUp;
    [SerializeField] private AudioSource _powerUpActivate;

    private bool _fadeOut;
    private bool _fadeIn;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        HandleMusicFading();
    }

    #region UTILITY

    public void SetMusicVolume(Slider musicSlider)
    {
        _audioSettings.SetMusicVolume(musicSlider);
        _audioMixer.SetFloat("musicVolume", Mathf.Log10(_audioSettings.MusicVolume) * 20);
    }

    public void SetEffectsVolume(Slider effectsSlider)
    {
        _audioSettings.SetEffectsVolume(effectsSlider);
        _audioMixer.SetFloat("effectsVolume", Mathf.Log10(_audioSettings.EffectsVolume) * 20);
    }

    #endregion

    #region MUSIC

    public void SetMusicVolume(float volume)
    {
        _fadeOut = false;
        _fadeIn = false;

        _music.volume = volume;
    }

    public void PlayMusic(AudioClip clip)
    {
        _music.clip = clip;

        if (!_music.isPlaying)
        {
            _music.Play();
        }
    }

    public void FadeMusicOut() => _fadeOut = true;
    public void FadeMusicIn() => _fadeIn = true;

    public void StopMusic() => _music.Stop();

    public void PlayAmbience(AudioClip clip)
    {
        _ambience.clip = clip;
        
        if (!_ambience.isPlaying)
        {
            _ambience.Play();
        }
    }

    public void StopAmbience() => _ambience.Stop();

    #endregion

    #region PLAYER

    public void PlayPlayerShoot(AudioClip clip) => _playerShoot.PlayOneShot(clip);

    public void PlayPlayerShipMovement(AudioClip clip)
    {
        if (_playerShipMovement.volume < 0.5f)
        {
            _playerShipMovement.volume += Time.deltaTime * 2;
        }

        if (!_playerShipMovement.isPlaying)
        {
            _playerShipMovement.PlayOneShot(clip);
        }
    }

    public void FadeOutPlayerShipMovement() => _playerShipMovement.volume -= Time.deltaTime;
    public void StopPlayerShipMovement() => _playerShipMovement.Stop();

    public void PlayPlayerWarp(AudioClip clip)
    {
        _playerWarp.PlayOneShot(clip);
    }

    public void PlayPlayerDeath(AudioClip clip)
    {
        _playerDeath.PlayOneShot(clip);
    }

    #endregion

    #region ENEMIES

    public void PlayEnemyShoot(AudioClip clip)
    {
        _enemyShoot.clip = clip;
        _enemyShoot.Play();
    }

    public void PlayEnemyDamaged(AudioClip clip)
    {
        _enemyDamaged.clip = clip;
        _enemyDamaged.Play();
    }

    public void PlayEnemyShipMovement(AudioClip clip)
    {
        if (!_enemyShipMovement.isPlaying)
        {
            _enemyShipMovement.PlayOneShot(clip);
        }
    }

    public void StopEnemyShipMovement() => _enemyShipMovement.Stop();

    #endregion

    #region ETC

    public void PlayPowerUpPickUp(AudioClip clip)
    {
        _powerUpPickUp.PlayOneShot(clip);
    }

    public void PlayPowerUpActivate(AudioClip clip)
    {
        _powerUpActivate.PlayOneShot(clip);
    }

    #endregion

    private void HandleMusicFading()
    {
        if (_fadeOut && _music.volume > 0 && !_fadeIn)
        {
            _music.volume -= Time.unscaledDeltaTime * 0.5f;
        }
        else
        {
            _fadeOut = false;
        }

        if (_fadeIn && _music.volume < 1)
        {
            _music.volume += Time.deltaTime * 0.75f;
        }
        else
        {
            _fadeIn = false;
        }
    }
}
