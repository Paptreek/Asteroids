using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "AudioSettings", menuName = "Scriptable Objects/AudioSettings")]
public class AudioSettings : ScriptableObject
{
    public float MusicVolume { get; private set; }
    public float EffectsVolume { get; private set; }

    public void SetMusicVolume(Slider volumeSlider)
    {
        MusicVolume = volumeSlider.value;
    }

    public void SetEffectsVolume(Slider volumeSlider)
    {
        EffectsVolume = volumeSlider.value;
    }
}
