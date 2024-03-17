using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Volume Sliders")]
    [SerializeField] public Slider musicVolumeSlider;
    [SerializeField] public Slider currentMusicVolumeSlider;
    [SerializeField] public Slider sfxVolumeSlider;
    [SerializeField] public Slider currentSFXVolumeSlider;


    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip mainMenuMusic, gameMusic; //bossMusic;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        SceneChanged();
    }

    private void Update()
    {
        if (SettingsScript.Instance.musicEnabled && !musicSource.isActiveAndEnabled)
        {
            musicSource.enabled = true;
        }
        else if (!SettingsScript.Instance.musicEnabled && musicSource.isActiveAndEnabled)
        {
            musicSource.enabled = false;
        }

        if (SettingsScript.Instance.sfxEnabled && !sfxSource.isActiveAndEnabled)
        {
            sfxSource.enabled = true;
        }
        else if (!SettingsScript.Instance.sfxEnabled && sfxSource.isActiveAndEnabled)
        {
            sfxSource.enabled = false;
        }
    }

    public void ChangeMusicVolume()
    {
        musicSource.volume = musicVolumeSlider.value;
        Save();
    }

    public void ChangeSFXVolume()
    {
        sfxSource.volume = sfxVolumeSlider.value;
        Save();
    }

    public void Load()
    {
        if (currentMusicVolumeSlider != null && PlayerPrefs.HasKey("musicVolume"))
        {
            currentMusicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }
        if (currentSFXVolumeSlider != null && PlayerPrefs.HasKey("sfxVolume"))
        {
            currentSFXVolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        }
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolumeSlider.value);
    }

    public void PlayMenuMusic()
    {
        musicSource.clip = mainMenuMusic;
        musicSource.Play();
    }
    public void PlayGameMusic()
    {
        musicSource.clip = gameMusic;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
    //public void PlayBossMusic()
    //{
    //    musicSource.clip = bossMusic;
    //    musicSource.Play();
    //}

    public void SceneChanged()
    {
        currentMusicVolumeSlider = musicVolumeSlider;
        currentSFXVolumeSlider = sfxVolumeSlider;
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0.3f);
        }
        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 0.3f);
        }
        Load();
        StopMusic();
        PlayMenuMusic();
    }
}
