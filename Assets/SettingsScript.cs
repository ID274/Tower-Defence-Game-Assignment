using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Security.Cryptography;

public class SettingsScript : MonoBehaviour
{
    public static SettingsScript main;

    public bool tutorialsEnabled;
    public bool sfxEnabled;
    public bool musicEnabled;
    public bool fullscreenEnabled;

    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private TextMeshProUGUI tutorialButton, fullscreenButton, sfxButton, musicButton;

    void Awake()
    {
        main = this;
        DontDestroyOnLoad(this.gameObject);
        if (!PlayerPrefs.HasKey("FullScreenEnabled"))
        {
            PlayerPrefs.SetFloat("FullScreenEnabled", 1);
        }
        if (!PlayerPrefs.HasKey("MusicEnabled"))
        {
            PlayerPrefs.SetFloat("MusicEnabled", 1);
        }
        if (!PlayerPrefs.HasKey("SFXEnabled"))
        {
            PlayerPrefs.SetFloat("SFXEnabled", 1);
        }
        if (!PlayerPrefs.HasKey("TutorialsEnabled"))
        {
            PlayerPrefs.SetFloat("TutorialsEnabled", 1);
        }
        LoadPrefs();
    }

    private void FixedUpdate()
    {
        if (tutorialsEnabled)
        {
            tutorialButton.text = "Tutorials: ON";
        }
        else
        {
            tutorialButton.text = "Tutorials: OFF";
        }
        if (fullscreenEnabled)
        {
            fullscreenButton.text = "Fullscreen: ON";
        }
        else
        {
            fullscreenButton.text = "Fullscreen: OFF";
        }
        if (sfxEnabled)
        {
            sfxButton.text = "SFX: ON";
        }
        else
        {
            sfxButton.text = "SFX: OFF";
        }
        if (musicEnabled)
        {
            musicButton.text = "Music: ON";
        }
        else
        {
            musicButton.text = "Music: OFF";
        }
    }
    public void ToggleTutorials()
    {
        if (tutorialsEnabled)
        {
            tutorialsEnabled = false;
        }
        else
        {
            tutorialsEnabled = true;
        }
    }
    public void ToggleFullScreen()
    {
        if (fullscreenEnabled)
        {
            fullscreenEnabled = false;
        }
        else
        {
            fullscreenEnabled = true;
        }
    }
    public void ToggleSFX()
    {
        if (sfxEnabled)
        {
            sfxEnabled = false;
        }
        else
        {
            sfxEnabled = true;
        }
    }
    public void ToggleMusic()
    {
        if (musicEnabled)
        {
            musicEnabled = false;
        }
        else
        {
            musicEnabled = true;
        }
    }
    public void DoneButton()
    {
        SavePrefs();
        settingsMenu.SetActive(false);
    }

    public void SavePrefs()
    {
        PlayerPrefs.SetInt("MusicEnabled", Convert.ToInt32(musicEnabled));
        PlayerPrefs.SetInt("SFXEnabled", Convert.ToInt32(sfxEnabled));
        PlayerPrefs.SetInt("FullscreenEnabled", Convert.ToInt32(fullscreenEnabled));
        PlayerPrefs.SetInt("TutorialsEnabled", Convert.ToInt32(tutorialsEnabled));
    }
    private void LoadFullscreenPrefs()
    {
        fullscreenEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenEnabled"));
    }
    private void LoadMusicPrefs()
    {
        musicEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("MusicEnabled"));
    }
    private void LoadSFXPrefs()
    {
        sfxEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("SFXEnabled"));
    }
    private void LoadTutorialPrefs()
    {
        tutorialsEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("TutorialsEnabled"));
    }

    private void LoadPrefs()
    {
        LoadMusicPrefs();
        LoadSFXPrefs();
        LoadFullscreenPrefs();
        LoadTutorialPrefs();
    }
}
