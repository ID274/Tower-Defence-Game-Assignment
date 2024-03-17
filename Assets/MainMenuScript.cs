using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;

    public void QuitButton()
    {
        Application.Quit();
    }
    public void SettingsButton()
    {
        settingsMenu.SetActive(true);
    }

    public void PlayButton()
    {
        SceneManagerScript.Instance.ChangeScenes();
    }
}
