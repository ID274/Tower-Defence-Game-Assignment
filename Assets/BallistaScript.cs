using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class BallistaScript : MonoBehaviour
{
    //Animation
    public SpriteRenderer spriteRenderer;
    public Sprite LoadedSprite, UnloadedSprite;
    [SerializeField] private bool shotFinished = true;

    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private LayerMask flyingEnemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float attackSpeed = 1f; //bullets per second

    private float preModAttackSpeed;
    private float preModRange;

    private Transform target;
    private float timeUntilFire;
    private float timeUntilFireHalf;


    private void Start()
    {
        preModAttackSpeed = attackSpeed;
        preModRange = targetingRange;
    }


    private void Update()
    {
        if (preModAttackSpeed * ModifierScript.Instance.attackSpeedMult != attackSpeed)
        {
            ModAttackSpeed();
        }
        if (preModRange * ModifierScript.Instance.rangeMult != targetingRange)
        {
            ModRange();
        }
        if (!LevelManager.main.gameOver)
        {
            if (target == null)
            {
                FindTargetAerial();
                if (target == null)
                {
                    FindTarget();
                }
                return;
            }
            RotateTowardsTarget();

            if (!CheckTargetIsInRange())
            {
                target = null;
            }
            else
            {
                timeUntilFire += Time.deltaTime;
                if (timeUntilFire >= 1f / attackSpeed)
                {
                    Shoot();
                }
            }
        }
        
    }

    private void Shoot()
    {

        if (shotFinished)
        {
            StartCoroutine(ShotAnimation());
            timeUntilFire = 0f;
        }
        else
        {
            return;
        }
    }

    public IEnumerator ShotAnimation()
    {
        timeUntilFireHalf = timeUntilFire / 2;
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        BallistaArrowScript bulletScript = bulletObj.GetComponent<BallistaArrowScript>();
        bulletScript.SetTarget(target);
        Debug.Log("Shoot");
        shotFinished = false;
        spriteRenderer.sprite = UnloadedSprite;
        yield return new WaitForSeconds(timeUntilFireHalf);
        spriteRenderer.sprite = LoadedSprite;
        yield return new WaitForSeconds(timeUntilFireHalf);
        shotFinished = true;
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }
    private void FindTargetAerial()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, flyingEnemyMask);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }

    void ModAttackSpeed()
    {
        attackSpeed = preModAttackSpeed * ModifierScript.Instance.attackSpeedMult;
    }

    void ModRange()
    {
        targetingRange = preModRange * ModifierScript.Instance.rangeMult;
    }
}

