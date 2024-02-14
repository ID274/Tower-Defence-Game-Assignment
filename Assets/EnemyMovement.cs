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
    [SerializeField] private bool flyingEnemy;
    private Transform target;
    private int pathIndex = 0;

    private void Start()
    {
        enemyHealthScript = this.GetComponent<EnemyHealth>();
        target = LevelManager.main.path[pathIndex];
    }
    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f && !LevelManager.main.gameOver)
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
    }
    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }
}
