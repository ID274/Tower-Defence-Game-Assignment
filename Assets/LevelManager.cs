using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform[] path;
    public Transform startPoint;
    public Transform[] pathAerial;
    public Transform startPointAerial;

    public int currentWave;
    public int enemiesKilled;
    public int currency;
    public int startCurrency = 450;
    public int health;
    public int startHealth = 100;

    [Header("Game Over")]
    public bool gameOver = false;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI diedOnWaveText, enemiesKilledText;

    [Header("Timescale References")]
    [SerializeField] private GameObject normalSpeedButton, doubleSpeedButton;


    private void Awake()
    {
        main = this;
        Time.timeScale = 1f;
        SoundManager.main.PlayGameMusic();
        Debug.Log("Start timescale" + "=" + Time.timeScale);
    }
    private void Start()
    {
        health = startHealth;
        currency = startCurrency;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            //BUY ITEM
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("Not enough money");
            return false;
        }
    }

    private void Update()
    {
        if (health <= 0)
        {
            gameOver = true;
            gameOverScreen.SetActive(true);
            diedOnWaveText.text = $"Survived until: Wave {currentWave}";
            enemiesKilledText.text = $"Enemies killed: {enemiesKilled}";
        }
        if (Time.timeScale == 1f)
        {
            normalSpeedButton.SetActive(false);
            doubleSpeedButton.SetActive(true);
        }
        else if (Time.timeScale == 2f || Time.timeScale == 0f)
        {
            normalSpeedButton.SetActive(true);
            doubleSpeedButton.SetActive(false);
        }
    }


    public void DoubleSpeed()
    {
        Time.timeScale = 2f;
        Debug.Log(Time.timeScale);
    }
    public void NormalSpeed()
    {
        Time.timeScale = 1f;
        Debug.Log(Time.timeScale);
    }

    public void Pause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            Debug.Log(Time.timeScale);
        }
        else
        {
            Time.timeScale = 0f;
            Debug.Log(Time.timeScale);
        }
    }
    public void RetryLevel()
    {
        SceneManagerScript.main.ReloadScene();
    }
}
