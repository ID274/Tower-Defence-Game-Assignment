using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;

    public void QuitButton()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.buttonClickSFX);
        Application.Quit();
    }
    public void SettingsButton()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.buttonClickSFX);
        settingsMenu.SetActive(true);
    }

    public void PlayButton()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.buttonClickSFX);
        SceneManagerScript.Instance.ChangeScenes();
    }
}
