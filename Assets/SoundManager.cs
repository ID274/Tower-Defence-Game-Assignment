using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [Header("Sound Effects")]
    public AudioClip arrowImpactSFX, spearSFX, cannonExplosionSFX, gameOverSFX, buttonClickSFX, leftClickSFX;

    [Header("Debug")]
    [SerializeField] float sfxSlider;
    [SerializeField] float musicSlider;


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
        Load();
        PlayMusic(mainMenuMusic);
    }

    private void Update()
    {
        if (currentMusicVolumeSlider != null)
        {
            musicSlider = currentMusicVolumeSlider.value;
        }
        if (currentSFXVolumeSlider != null)
        {
            sfxSlider = currentSFXVolumeSlider.value;
        }
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
        Load();
    }

    public void ChangeSFXVolume()
    {
        sfxSource.volume = sfxVolumeSlider.value;
        Save();
        Load();
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

    public void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolumeSlider.value);
    }
    public void PlayMusic(AudioClip music)
    {
        musicSource.PlayOneShot(music);
    }
    public void PlaySFX(AudioClip sfx)
    {
        sfxSource.PlayOneShot(sfx);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void StopSFX()
    {
        sfxSource.Stop();
    }
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
        StopSFX();
        if (SceneManager.GetSceneByBuildIndex(1).isLoaded)
        {
            StopMusic();
            Debug.Log("Main Menu - scene loaded, loaded scenes: " + SceneManager.loadedSceneCount);
            Load();
            PlayMusic(mainMenuMusic);
        }
        else if (SceneManager.GetSceneByBuildIndex(0).isLoaded)
        {
            StopMusic();
            Debug.Log("Endless Map 1 - scene loaded, loaded scenes: " + SceneManager.loadedSceneCount);
            Load();
            PlayMusic(gameMusic);
        }
    }
}
