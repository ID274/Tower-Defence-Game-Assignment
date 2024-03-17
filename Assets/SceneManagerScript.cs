using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript Instance { get; private set; }
    private Scene currentScene;
    public int buildIndex;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
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
            SoundManager.Instance.StopMusic();
            SoundManager.Instance.PlayGameMusic();
        }
        else if (buildIndex == 1)
        {
            buildIndex = 0;
            SoundManager.Instance.StopMusic();
        }
        SoundManager.Instance.SceneChanged();
        SceneManager.LoadScene(buildIndex);
        currentScene = SceneManager.GetActiveScene();
        buildIndex = currentScene.buildIndex;
    }
}
