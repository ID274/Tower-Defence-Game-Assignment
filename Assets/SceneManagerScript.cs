using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    private Scene currentScene;
    private string sceneName;
    private int buildIndex;


    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        buildIndex = currentScene.buildIndex;
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeScenes()
    {
        if (buildIndex == 0)
        {
            buildIndex = 1;
            SoundManager.main.PlayGameMusic();
        }
        else if (buildIndex == 1)
        {
            buildIndex = 0;
            SoundManager.main.PlayMenuMusic();
        }
        SceneManager.LoadScene(buildIndex);
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        buildIndex = currentScene.buildIndex;

    }




}
