using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Security.Cryptography;

public class SettingsScript : MonoBehaviour
{
    public static SettingsScript Instance { get; private set; }

    public bool tutorialsEnabled;
    public bool sfxEnabled;
    public bool musicEnabled;

    void Awake()
    {
        Screen.fullScreen = !Screen.fullScreen;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
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
        LoadPrefs();
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
