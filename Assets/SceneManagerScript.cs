using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript main;
    private Scene currentScene;
    public int buildIndex;


    private void Awake()
    {
        main = this;
        DontDestroyOnLoad(this.gameObject);
        currentScene = SceneManager.GetActiveScene();
        buildIndex = currentScene.buildIndex;
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeScenes()
    {
        currentScene = SceneManager.GetActiveScene();
        buildIndex = currentScene.buildIndex;
        if (buildIndex == 0)
        {
            buildIndex = 1;
            SoundManager.main.StopMusic();
            SoundManager.main.PlayGameMusic();
        }
        else if (buildIndex == 1)
        {
            buildIndex = 0;
            SoundManager.main.StopMusic();
        }
        SceneManager.LoadScene(buildIndex);
        currentScene = SceneManager.GetActiveScene();
        buildIndex = currentScene.buildIndex;
        SoundManager.main.volumeSlider = FindAnyObjectByType<Slider>();
    }

    




}
