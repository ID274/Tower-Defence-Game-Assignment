using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public static MainMenuScript main;

    [SerializeField] private GameObject settingsMenu;

    void Awake()
    {
        main = this;
    }

    public void QuitButton()
    {
        Application.Quit();
    }
    public void SettingsButton()
    {
        settingsMenu.SetActive(true);
    }
}
