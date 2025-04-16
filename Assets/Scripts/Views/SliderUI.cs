using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioManager audioManager; // Drag dari Inspector
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        musicSlider.value = audioManager.musicVolume;
        sfxSlider.value = audioManager.sfxVolume;

        musicSlider.onValueChanged.AddListener(delegate { ChangeMusicVolume(); });
        sfxSlider.onValueChanged.AddListener(delegate { ChangeSFXVolume(); });
    }

    public void ChangeMusicVolume()
    {
        audioManager.SetMusicVolume(musicSlider.value);
    }

    public void ChangeSFXVolume()
    {
        audioManager.SetSFXVolume(sfxSlider.value);
    }
}
