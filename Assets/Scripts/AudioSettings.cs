using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "AudioSettings", menuName = "Scriptable Objects/AudioSettings")]
public class AudioSettings : ScriptableObject
{
    public float MusicVolume { get; private set; } = 1;
    public float EffectsVolume { get; private set; } = 1;

    public void SetMusicVolume(Slider volumeSlider)
    {
        MusicVolume = volumeSlider.value;
    }

    public void SetEffectsVolume(Slider volumeSlider)
    {
        EffectsVolume = volumeSlider.value;
    }
}
