using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSettings : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu, keybindsMenu;
    [SerializeField] private TextMeshProUGUI tutorialButton, fullscreenButton, sfxButton, musicButton;
    [SerializeField] private Slider musicVolumeSlider, sfxVolumeSlider;

    void Start()
    {
        fullscreenButton.text = "Fullscreen";
        SoundManager.Instance.musicVolumeSlider = musicVolumeSlider;
        SoundManager.Instance.currentMusicVolumeSlider = musicVolumeSlider;
        SoundManager.Instance.sfxVolumeSlider = sfxVolumeSlider;
        SoundManager.Instance.currentSFXVolumeSlider = sfxVolumeSlider;
        SoundManager.Instance.Load();
    }


    void Update()
    {
        if (ControlsScript.Instance.toggleSettingsPressed)
        {
            switch (settingsMenu.activeSelf)
            {
                case true:
                    SettingsScript.Instance.SavePrefs();
                    SettingsScript.Instance.LoadPrefs();
                    keybindsMenu.SetActive(false);
                    settingsMenu.SetActive(false);
                    break;
                case false:
                    settingsMenu.SetActive(true);
                    break;
            }
        }

        if (SettingsScript.Instance.tutorialsEnabled && tutorialButton != null)
        {
            tutorialButton.text = "Tutorials: ON";
        }
        else if (tutorialButton != null)
        {
            tutorialButton.text = "Tutorials: OFF";
        }
        if (SettingsScript.Instance.sfxEnabled && sfxButton != null)
        {
            sfxButton.text = "SFX: ON";
        }
        else if (sfxButton != null)
        {
            sfxButton.text = "SFX: OFF";
        }
        if (SettingsScript.Instance.musicEnabled && musicButton != null)
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
        if (SettingsScript.Instance.tutorialsEnabled)
        {
            SettingsScript.Instance.tutorialsEnabled = false;
        }
        else
        {
            SettingsScript.Instance.tutorialsEnabled = true;
        }
        SettingsScript.Instance.SavePrefs();
        SettingsScript.Instance.LoadPrefs();
    }
    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void ToggleSFX()
    {
        if (SettingsScript.Instance.sfxEnabled)
        {
            SettingsScript.Instance.sfxEnabled = false;
        }
        else
        {
            SettingsScript.Instance.sfxEnabled = true;
        }
        SettingsScript.Instance.SavePrefs();
        SettingsScript.Instance.LoadPrefs();
    }
    public void ToggleMusic()
    {
        if (SettingsScript.Instance.musicEnabled)
        {
            SettingsScript.Instance.musicEnabled = false;
        }
        else
        {
            SettingsScript.Instance.musicEnabled = true;
        }
        SettingsScript.Instance.SavePrefs();
        SettingsScript.Instance.LoadPrefs();
    }
    public void DoneButton()
    {
        SettingsScript.Instance.SavePrefs();
        SettingsScript.Instance.LoadPrefs();
        settingsMenu.SetActive(false);
    }

    public void KeyBindsButton()
    {
        switch (keybindsMenu.activeSelf)
        {
            case true:
                keybindsMenu.SetActive(false);
                break;
            case false:
                keybindsMenu.SetActive(true);
                break;
        }
    }


    public void ChangeMusicVolume()
    {
        SoundManager.Instance.ChangeMusicVolume();
    }
    public void ChangeSFXVolume()
    {
        SoundManager.Instance.ChangeSFXVolume();
    }
}
