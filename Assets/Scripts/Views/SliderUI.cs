using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        // Set sliders to current volume values
        musicSlider.value = AudioManager.instance.musicVolume;
        sfxSlider.value = AudioManager.instance.sfxVolume;

        // Add listener
        musicSlider.onValueChanged.AddListener(delegate { ChangeMusicVolume(); });
        sfxSlider.onValueChanged.AddListener(delegate { ChangeSFXVolume(); });
    }

    public void ChangeMusicVolume()
    {
        AudioManager.instance.SetMusicVolume(musicSlider.value);
    }

    public void ChangeSFXVolume()
    {
        AudioManager.instance.SetSFXVolume(sfxSlider.value);
    }
}
