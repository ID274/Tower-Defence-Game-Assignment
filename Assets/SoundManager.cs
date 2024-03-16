using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager main;

    [Header("Volume Slider")]
    [SerializeField] public Slider volumeSlider;
    [SerializeField] public Slider currentVolumeSlider;

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip mainMenuMusic, gameMusic; //bossMusic;


    void Awake()
    {
        main = this;
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        currentVolumeSlider = volumeSlider;
        if (!PlayerPrefs.HasKey("soundVolume"))
        {
            PlayerPrefs.SetFloat("soundVolume", 0.3f);
        }
        Load();
        StopMusic();
        PlayMenuMusic();
    }

    private void Update()
    {
        if (SettingsScript.main.musicEnabled && !musicSource.isActiveAndEnabled)
        {
            musicSource.enabled = true;
        }
        else if (!SettingsScript.main.musicEnabled && musicSource.isActiveAndEnabled)
        {
            musicSource.enabled = false;
        }

        if (SettingsScript.main.sfxEnabled && !sfxSource.isActiveAndEnabled)
        {
            sfxSource.enabled = true;
        }
        else if (!SettingsScript.main.sfxEnabled && sfxSource.isActiveAndEnabled)
        {
            sfxSource.enabled = false;
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    public void Load()
    {
        currentVolumeSlider.value = PlayerPrefs.GetFloat("soundVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("soundVolume", volumeSlider.value);
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
}
