using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject[] bossPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 0;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    private int bossIndex = 0;
    private bool bossSpawned = false;
    


    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        currentWave = 0;
        bossSpawned = false;
        StartWave();
        StartCoroutine(StartWave());
    }
    private void Update()
    {
        if (!LevelManager.main.gameOver)
        {
            while (bossIndex > bossPrefabs.Length - 1)
            {
                bossIndex--;
            }
            if (!isSpawning) return;
            timeSinceLastSpawn += Time.deltaTime;
            if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
            {
                SpawnEnemy();
                enemiesLeftToSpawn--;
                enemiesAlive++;
                timeSinceLastSpawn = 0f;
                ;
            }

            if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
            {
                EndWave();
            }
        }
    }

    private void SpawnEnemy()
    {
        if ((currentWave / 2) % 1 != 0 || bossPrefabs[0] == null || bossSpawned == true)
        {
            if (currentWave < 2)
            {
                GameObject prefabToSpawn = enemyPrefabs[0];
                Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
            }
            else
            {
                int rnd = Random.Range(0, 2);
                GameObject prefabToSpawn = enemyPrefabs[rnd];
                Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
            }
        }
        else if (bossPrefabs[bossIndex] != null && bossSpawned == false)
        {
            bossSpawned = true;
            SpawnBoss();
        }
        
        
    }

    private void SpawnBoss()
    {


        GameObject bossToSpawn = bossPrefabs[bossIndex];
        GameObject bossInstance = Instantiate(bossToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
        EnemyMovement bossMovement = bossInstance.GetComponent<EnemyMovement>();
        EnemyHealth bossHealth = bossInstance.GetComponent<EnemyHealth>();

        bossMovement.moveSpeed /= 2;
        bossHealth.damageToBase *= 20;
        bossHealth.hitPoints *= 20 * currentWave;
        bossHealth.isBoss = true;
        bossHealth.currencyWorth *= currentWave;
        bossIndex++;

    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        currentWave++;
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        LevelManager.main.currency += currentWave * 100 / 2;
        bossSpawned = false;
        StartCoroutine(StartWave());

    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
}
