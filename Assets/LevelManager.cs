using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform[] path;
    public Transform startPoint;

    public int currency;
    public int startCurrency = 450;
    public int health;
    public int startHealth = 100;

    [Header("Game Over")]
    public bool gameOver = false;

    private void Awake()
    {
        main = this;
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
    }
}
