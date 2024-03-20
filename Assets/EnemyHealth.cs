using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Attributes")]
    public float hitPoints = 2f;
    public int currencyWorth = 50;
    public bool isBoss = false;
    public int damageToBase = 1;

    private int preModCurrencyWorth;


    private void Start()
    {
        preModCurrencyWorth = currencyWorth;
    }

    public bool isDestroyed = false;
    public void TakeDamage(float damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.Instance.enemiesKilled++;
            if (isBoss)
            {
                LevelManager.Instance.bossesKilled++;
            }
            LevelManager.Instance.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            LevelManager.Instance.CalculateScore();
            Destroy(gameObject);
        }
        
    }

    private void Update()
    {
        if (currencyWorth != preModCurrencyWorth * ModifierScript.Instance.goldGainMult)
        {
            ModMoney();
        }
        if (LevelManager.Instance.gameOver)
        {
            currencyWorth = 0;
            Destroy(gameObject);
        }
    }

    private void ModMoney()
    {
        currencyWorth = Mathf.RoundToInt(preModCurrencyWorth * ModifierScript.Instance.goldGainMult);
    }
}
