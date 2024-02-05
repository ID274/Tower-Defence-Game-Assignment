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
    private void Awake()
    {
        main = this;
    }
    private void Start()
    {
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
}
