using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Attributes")]
    public int hitPoints = 2;
    public int currencyWorth = 50;
    public bool isBoss = false;
    public int damageToBase = 1;

    public bool isDestroyed = false;
    public void TakeDamage(int damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
        
    }

    private void Update()
    {
        if (LevelManager.main.gameOver)
        {
            currencyWorth = 0;
            Destroy(gameObject);
        }
    }
}
