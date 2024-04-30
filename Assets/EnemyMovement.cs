using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite enemySprite;
    [SerializeField] private Sprite[] animSprites;
    [SerializeField] private Rigidbody2D rb;

    private EnemyHealth enemyHealthScript;


    [Header("Attributes")]
    [SerializeField] public float moveSpeed = 2f;
    [SerializeField] public float originalMoveSpeed;
    [SerializeField] public GameObject slowedByGameObject;
    public bool flyingEnemy;
    private Transform target;
    private Transform lastTarget;
    private int pathIndex = 0;
    private int pathIndexAerial = 0;

    public bool slowed;
    public bool boss;

    private void Awake()
    {
        if (boss)
        {
            moveSpeed += 0.05f * EnemySpawner.Instance.currentWave;
            moveSpeed = moveSpeed / 2;
        }
        else
        {
            moveSpeed += 0.05f * EnemySpawner.Instance.currentWave;
        }
        originalMoveSpeed = moveSpeed;
        if (animSprites.Length != 0)
        {
            spriteRenderer.color = Color.white;
            StartCoroutine(MovementAnimation());
        }
        
    }
    private void Start()
    {
        if (!flyingEnemy)
        {
            enemyHealthScript = this.GetComponent<EnemyHealth>();
            target = LevelManager.Instance.path[pathIndex];
            lastTarget = LevelManager.Instance.beforePath;
        }
        else
        {
            enemyHealthScript = this.GetComponent<EnemyHealth>();
            target = LevelManager.Instance.pathAerial[pathIndexAerial];
            lastTarget = LevelManager.Instance.beforePathAerial;
        }
        Debug.LogWarning(lastTarget);
        ChangeRotation();
    }
    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f && !LevelManager.Instance.gameOver && !flyingEnemy)
        {
            pathIndex++;

            if (pathIndex == LevelManager.Instance.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                LevelManager.Instance.health -= enemyHealthScript.damageToBase;
                return;
            }
            else
            {
                target = LevelManager.Instance.path[pathIndex];
            }
        }
        else if (Vector2.Distance(target.position, transform.position) <= 0.1f && !LevelManager.Instance.gameOver && flyingEnemy)
        {
            pathIndexAerial++;

            if (pathIndexAerial == LevelManager.Instance.pathAerial.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                LevelManager.Instance.health -= enemyHealthScript.damageToBase;
                return;
            }
            else
            {
                target = LevelManager.Instance.pathAerial[pathIndexAerial];
            }
        }
    }
    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    private IEnumerator MovementAnimation()
    {
        foreach (Sprite sprite in animSprites)
        {
            spriteRenderer.sprite = sprite;
            yield return new WaitForSeconds(0.3f / moveSpeed);
        }
        StartCoroutine(MovementAnimation());
    }

    public void ChangeRotation()
    {
        if (target.position.x > lastTarget.position.x)
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, -90);
        }
        else if (target.position.x < lastTarget.position.x)
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 90);
        }
        if (target.position.y > lastTarget.position.y)
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 0);
        }
        else if (target.position.y < lastTarget.position.y)
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 180);
        }
    }
}
