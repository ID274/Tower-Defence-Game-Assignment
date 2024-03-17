using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

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
    [SerializeField] public int currentWave = 0;
    [SerializeField] private int orderInLayerCount = 10;
    public int flyingEnemyAmount;
    public int toSpawnCount;

    private int previousWave;



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
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
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
        if (!LevelManager.Instance.gameOver)
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
            if (enemiesAlive == 0 && enemiesLeftToSpawn == 0 && !waveEnded)
            {
                waveEnded = true;
                if (!powerupScreen.activeSelf && currentWave % 5 == 0)
                {
                    powerupScreen.SetActive(true);
                }
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
                EnemyHealth prefabInstance = Instantiate(prefabToSpawn, LevelManager.Instance.startPoint.position, Quaternion.identity);
                prefabInstance.hitPoints = (2 * currentWave) / 1.2f;
                SpriteRenderer prefabSprite = prefabInstance.GetComponent<SpriteRenderer>();
                prefabSprite.sortingOrder = orderInLayerCount;
                orderInLayerCount++;
                enemiesAlive++;


            }
            else
            {
                if (flyingEnemyAmount > 0)
                {
                    EnemyHealth flyingToSpawn = enemyPrefabs[1];
                    EnemyHealth flyingInstance = Instantiate(flyingToSpawn, LevelManager.Instance.startPointAerial.position, Quaternion.identity);
                    SpriteRenderer flyingSprite = flyingInstance.GetComponent<SpriteRenderer>();
                    flyingSprite.sortingOrder = orderInLayerCount * 5;
                    orderInLayerCount++;
                    flyingInstance.hitPoints = (2 * currentWave) / 1.2f;
                    flyingEnemyAmount--;
                    enemiesAlive++;

                }
                EnemyHealth prefabToSpawn = enemyPrefabs[0];
                EnemyHealth prefabInstance = Instantiate(prefabToSpawn, LevelManager.Instance.startPoint.position, Quaternion.identity);
                SpriteRenderer prefabSprite = prefabInstance.GetComponent<SpriteRenderer>();
                prefabSprite.sortingOrder = orderInLayerCount;
                orderInLayerCount++;
                prefabInstance.hitPoints = (2 * currentWave) / 1.2f;
                enemiesAlive++;
            }

        }


    }

    private void SpawnBoss()
    {


        GameObject bossToSpawn = bossPrefabs[bossIndex];
        GameObject bossInstance = Instantiate(bossToSpawn, LevelManager.Instance.startPoint.position, Quaternion.identity);
        EnemyHealth bossHealth = bossInstance.GetComponent<EnemyHealth>();
        SpriteRenderer bossSprite = bossInstance.GetComponent<SpriteRenderer>();
        bossSprite.sortingOrder = orderInLayerCount * 10;

        bossHealth.hitPoints = 30 * currentWave;
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
        orderInLayerCount = 10;
        LevelManager.Instance.currentWave = currentWave;
        if (currentWave == 1)
        {
            Debug.Log("current wave = 1");
            LevelManager.Instance.Pause();
            Debug.Log(SettingsScript.Instance.tutorialsEnabled);
            if (SettingsScript.Instance.tutorialsEnabled)
            {
                tutorialMenu.SetActive(true);
                Debug.Log($"Tutorials enabled: {SettingsScript.Instance.tutorialsEnabled} (IF)");
            }
            else
            {
                Debug.Log($"Tutorials enabled: {SettingsScript.Instance.tutorialsEnabled} (ELSE)");
            }
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
        LevelManager.Instance.currency += currentWave * 100 / 2;
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