using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Range(0f, 1f)]
    public float musicVolume = 1f;
    [Range(0f, 1f)]
    public float sfxVolume = 1f;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        FindAudioSources();
        ApplyVolume();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindAudioSources();
        ApplyVolume();
    }

    void FindAudioSources()
    {
        // Find Music Source
        GameObject musicObj = GameObject.FindWithTag("MusicManager");
        if (musicObj != null)
            musicSource = musicObj.GetComponent<AudioSource>();

        // Find SFX Source
        GameObject sfxObj = GameObject.FindWithTag("SFXManager");
        if (sfxObj != null)
            sfxSource = sfxObj.GetComponent<AudioSource>();
    }

    public void SetMusicVolume(float newVolume)
    {
        musicVolume = newVolume;
        if (musicSource != null)
            musicSource.volume = musicVolume;
    }

    public void SetSFXVolume(float newVolume)
    {
        sfxVolume = newVolume;
        if (sfxSource != null)
            sfxSource.volume = sfxVolume;
    }

    void ApplyVolume()
    {
        if (musicSource != null)
            musicSource.volume = musicVolume;
        if (sfxSource != null)
            sfxSource.volume = sfxVolume;
    }
}
    