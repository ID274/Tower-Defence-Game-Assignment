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

    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private TextMeshProUGUI tutorialButton, fullscreenButton, sfxButton, musicButton;

    void Awake()
    {
        main = this;
        DontDestroyOnLoad(this.gameObject);
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
        fullscreenButton.text = "Fullscreen";
        LoadPrefs();
    }

    private void Update()
    {
        if (tutorialsEnabled && tutorialButton != null)
        {
            tutorialButton.text = "Tutorials: ON";
        }
        else if (tutorialButton != null)
        {
            tutorialButton.text = "Tutorials: OFF";
        }
        if (sfxEnabled && sfxButton != null)
        {
            sfxButton.text = "SFX: ON";
        }
        else if (sfxButton != null)
        {
            sfxButton.text = "SFX: OFF";
        }
        if (musicEnabled && musicButton != null)
        {
            musicButton.text = "Music: ON";
        }
        else if (musicButton != null)
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
        Screen.fullScreen = !Screen.fullScreen;
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
        PlayerPrefs.SetInt("TutorialsEnabled", Convert.ToInt32(tutorialsEnabled));
    }
    //private void LoadFullscreenPrefs()
    //{
    //    fullscreenEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenEnabled"));
    //}
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

    public void LoadPrefs()
    {
        LoadMusicPrefs();
        LoadSFXPrefs();
        //LoadFullscreenPrefs();
        LoadTutorialPrefs();
    }
}
