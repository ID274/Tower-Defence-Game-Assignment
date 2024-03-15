using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyHealth[] enemyPrefabs;
    [SerializeField] private GameObject[] bossPrefabs;
    [SerializeField] private GameObject powerupScreen, tutorialMenu;
    [SerializeField] private GameObject groundWarning, groundBossWarning, flyingWarning, flyingBossWarning;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private int currentWave = 0;
    public int flyingEnemyAmount;
    public int toSpawnCount;



    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private float timeSinceLastSpawn;
    [SerializeField] private int enemiesAlive;
    [SerializeField] private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    private int bossIndex = 0;
    private bool bossSpawned = false;
    private bool waveEnded = false;
    public bool checkingTimescale;



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
        if (checkingTimescale)
        {
            CheckTimescale();
        }
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
                timeSinceLastSpawn = 0f;
            }
            if (enemiesAlive == 0 && enemiesLeftToSpawn == 0 && !powerupScreen.activeSelf && !waveEnded)
            {
                powerupScreen.SetActive(true);
                waveEnded = true;
            }

            if (waveEnded && !powerupScreen.activeSelf)
            {
                EndWave();
            }
        }
    }

    private void SpawnEnemy()
    {
        if (currentWave % 10 == 0 && bossPrefabs[bossIndex] != null && bossSpawned == false && toSpawnCount / 2 >= enemiesLeftToSpawn)
        {
            bossSpawned = true;
            SpawnBoss();
        }
        else if (bossPrefabs[0] == null || bossSpawned == true || currentWave % 10 != 0 || toSpawnCount / 2 < enemiesLeftToSpawn)
        {
            if (currentWave < 5)
            {
                EnemyHealth prefabToSpawn = enemyPrefabs[0];
                EnemyHealth prefabInstance = Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
                prefabInstance.hitPoints = 2 * currentWave;
                enemiesAlive++;


            }
            else
            {
                if (flyingEnemyAmount > 0)
                {
                    EnemyHealth flyingToSpawn = enemyPrefabs[1];
                    EnemyHealth flyingInstance = Instantiate(flyingToSpawn, LevelManager.main.startPointAerial.position, Quaternion.identity);
                    flyingInstance.hitPoints = 2 * currentWave;
                    flyingEnemyAmount--;
                    enemiesAlive++;

                }
                EnemyHealth prefabToSpawn = enemyPrefabs[0];
                EnemyHealth prefabInstance = Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
                prefabInstance.hitPoints = 2 * currentWave;
                enemiesAlive++;
            }

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
        enemiesAlive++;
        bossIndex++;

    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        waveEnded = false;
        currentWave++;
        if (currentWave == 1 || currentWave == 10)
        {
            Debug.Log("current wave = 1 or 10");
            LevelManager.main.Pause();
            tutorialMenu.SetActive(true);
            Debug.Log(Time.timeScale);
        }
        if (currentWave % 10 == 0 && bossPrefabs[bossIndex] != null)
        {
            groundBossWarning.SetActive(true);
        }
        else
        {
            groundWarning.SetActive(true);
        }
        if (currentWave >= 5)
        {
            flyingWarning.SetActive(true);
        }
        yield return new WaitForSeconds(timeBetweenWaves);
        groundBossWarning.SetActive(false);
        groundWarning.SetActive(false);
        flyingWarning.SetActive(false);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        toSpawnCount = enemiesLeftToSpawn;
        flyingEnemyAmount = Mathf.RoundToInt(enemiesLeftToSpawn / 5);
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

    private void CheckTimescale()
    {
        Debug.Log(Time.timeScale);
        checkingTimescale = false;
    }
}