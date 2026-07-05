using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioSettings _audioSettings;

    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _playerShoot;
    [SerializeField] private AudioSource _enemyExplosion;
    [SerializeField] private AudioSource _shipMovement;
    [SerializeField] private AudioSource _powerUpPickUp;
    [SerializeField] private AudioSource _powerUpActivate;

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

    public void SetMusicLowpass(float cutoff)
    {
        _audioMixer.SetFloat("musicLowpassCutoff", cutoff);
    }

    public void PlayMusic(AudioClip clip)
    {
        _music.clip = clip;

        if (!_music.isPlaying)
        {
            _music.Play();
        }
    }

    public void PlayPlayerShoot(AudioClip clip) => _playerShoot.PlayOneShot(clip);

    public void PlayEnemyExplosion(AudioClip clip)
    {
        _enemyExplosion.clip = clip;
        _enemyExplosion.Play();
    }

    public void PlayShipMove(AudioClip clip)
    {
        if (!_shipMovement.isPlaying)
        {
            _shipMovement.PlayOneShot(clip);
        }
    }

    public void StopShipMove(AudioClip clip)
    {
        _shipMovement.Stop();
    }

    public void PlayPowerUpPickUp(AudioClip clip)
    {
        _powerUpPickUp.PlayOneShot(clip);
    }

    public void PlayPowerUpActivate(AudioClip clip)
    {
        _powerUpActivate.PlayOneShot(clip);
    }
}
