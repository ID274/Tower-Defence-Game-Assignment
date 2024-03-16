using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    private EnemyHealth enemyHealthScript;


    [Header("Attributes")]
    [SerializeField] public float moveSpeed = 2f;
    [SerializeField] public float originalMoveSpeed;
    [SerializeField] public GameObject slowedByGameObject;
    public bool flyingEnemy;
    private Transform target;
    private int pathIndex = 0;
    private int pathIndexAerial = 0;




    public bool slowed;
    private void Awake()
    {
        originalMoveSpeed = moveSpeed;
    }
    private void Start()
    {
        if (!flyingEnemy)
        {
            enemyHealthScript = this.GetComponent<EnemyHealth>();
            target = LevelManager.main.path[pathIndex];
        }
        else
        {
            enemyHealthScript = this.GetComponent<EnemyHealth>();
            target = LevelManager.main.pathAerial[pathIndexAerial];
        }
    }
    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f && !LevelManager.main.gameOver && !flyingEnemy)
        {
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                LevelManager.main.health -= enemyHealthScript.damageToBase;
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
        else if (Vector2.Distance(target.position, transform.position) <= 0.1f && !LevelManager.main.gameOver && flyingEnemy)
        {
            pathIndexAerial++;

            if (pathIndexAerial == LevelManager.main.pathAerial.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                LevelManager.main.health -= enemyHealthScript.damageToBase;
                return;
            }
            else
            {
                target = LevelManager.main.pathAerial[pathIndexAerial];
            }
        }
    }
    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }
}
