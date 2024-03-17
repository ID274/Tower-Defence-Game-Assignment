using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fullscreenButton, sfxButton, musicButton;
    [SerializeField] private Slider musicVolumeSlider, sfxVolumeSlider;


    public GameObject settingsMenu;
    private float tempTimescale;

    private void Awake()
    {
        SettingsScript.Instance.LoadPrefs();
        fullscreenButton.text = "Fullscreen";
        SoundManager.Instance.musicVolumeSlider = musicVolumeSlider;
        SoundManager.Instance.currentMusicVolumeSlider = musicVolumeSlider;
        SoundManager.Instance.sfxVolumeSlider = sfxVolumeSlider;
        SoundManager.Instance.currentSFXVolumeSlider = sfxVolumeSlider;
        SoundManager.Instance.Load();
    }
    private void Update()
    {
        if (settingsMenu.activeSelf)
        {
            tempTimescale = Time.timeScale;
            Time.timeScale = 0f;
        }
        if (SettingsScript.Instance.sfxEnabled)
        {
            sfxButton.text = "SFX: ON";
        }
        else
        {
            sfxButton.text = "SFX: OFF";
        }
        if (SettingsScript.Instance.musicEnabled)
        {
            musicButton.text = "Music: ON";
        }
        else
        {
            musicButton.text = "Music: OFF";
        }
    }
    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
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
    public void DoneButton()
    {
        SettingsScript.Instance.SavePrefs();
        SettingsScript.Instance.LoadPrefs();
        Time.timeScale = tempTimescale;
        settingsMenu.SetActive(false);
    }

    public void QuitButton()
    {
        SettingsScript.Instance.SavePrefs();
        Application.Quit();
    }
    public void QuitToMainMenu()
    {
        SettingsScript.Instance.SavePrefs();
        SettingsScript.Instance.LoadPrefs();
        Debug.Log(SceneManagerScript.Instance.buildIndex + "build index");
        SceneManagerScript.Instance.ChangeScenes();
        Debug.Log(SceneManagerScript.Instance.buildIndex + "build index");
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
