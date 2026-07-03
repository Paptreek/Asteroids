using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioSettings _audioSettings;
    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _soundEffect;

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
        _audioMixer.SetFloat("effectsVolume", Mathf.Log10(_audioSettings.MusicVolume) * 20);
    }

    public void PlayMusic(AudioClip clip)
    {
        _music.clip = clip;
        _music.Play();
    }
}
