using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform[] path;
    public Transform startPoint;
    public Transform[] pathAerial;
    public Transform startPointAerial;


    public int currency;
    public int startCurrency = 450;
    public int health;
    public int startHealth = 100;

    [Header("Game Over")]
    public bool gameOver = false;

    [Header("Timescale References")]
    [SerializeField] private GameObject normalSpeedButton, doubleSpeedButton;


    private void Awake()
    {
        main = this;
        Time.timeScale = 1f;
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
}
