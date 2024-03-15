using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fullscreenButton, sfxButton, musicButton;
    public static GameSettingsScript main;

    public Slider volumeSlider;
    public GameObject settingsMenu;

    private void Awake()
    {
        SettingsScript.main.LoadPrefs();
        SoundManager.main.Load();
        fullscreenButton.text = "Fullscreen";
    }
    private void Update()
    {
        
        if (SettingsScript.main.sfxEnabled)
        {
            sfxButton.text = "SFX: ON";
        }
        else
        {
            sfxButton.text = "SFX: OFF";
        }
        if (SettingsScript.main.musicEnabled)
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
        SettingsScript.main.ToggleFullScreen();
        SettingsScript.main.SavePrefs();
        SettingsScript.main.LoadPrefs();
    }
    public void ToggleMusic()
    {
        if (SettingsScript.main.musicEnabled)
        {
            SettingsScript.main.musicEnabled = false;
        }
        else
        {
            SettingsScript.main.musicEnabled = true;
        }
        SettingsScript.main.SavePrefs();
        SettingsScript.main.LoadPrefs();
    }
    public void ToggleSFX()
    {
        if (SettingsScript.main.sfxEnabled)
        {
            SettingsScript.main.sfxEnabled = false;
        }
        else
        {
            SettingsScript.main.sfxEnabled = true;
        }
        SettingsScript.main.SavePrefs();
        SettingsScript.main.LoadPrefs();
    }
    public void DoneButton()
    {
        SettingsScript.main.SavePrefs();
        settingsMenu.SetActive(false);
    }

    public void QuitButton()
    {
        SettingsScript.main.SavePrefs();
        Application.Quit();
    }
    public void QuitToMainMenu()
    {
        SettingsScript.main.SavePrefs();
        Debug.Log(SceneManagerScript.main.buildIndex + "build index");
        SceneManagerScript.main.ChangeScenes();
        Debug.Log(SceneManagerScript.main.buildIndex + "build index");
    }


}
