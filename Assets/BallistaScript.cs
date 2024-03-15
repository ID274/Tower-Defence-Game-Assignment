using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;

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
    [SerializeField] private SpriteRenderer towerBase;
    [SerializeField] private Sprite upgradedSprite;
    [SerializeField] public SpriteRenderer rangeIndicator;

    [Header("Attributes for stats window")]
    public int attackCount;
    public float damageDealt;

    [Header("Attributes")]
    public float targetingRange = 5f;
    public float damage = 3;
    //[SerializeField] private float rotationSpeed = 10f;
    public float attackSpeed = 1f; //bullets per second
    public int upgradePath = 0;
    public int upgrade1Count = 0;
    public int upgrade2Count = 0;
    public int upgradeCount;

    public float preModDamage;
    public float preModAttackSpeed;
    public float preModRange;

    private Transform target;
    private float timeUntilFire;
    private float timeUntilFireHalf;
    public bool aerial;

    public Quaternion towerPointingDirection;



    private void Start()
    {
        preModDamage = damage;
        preModAttackSpeed = attackSpeed;
        preModRange = targetingRange;
    }


    private void Update()
    {
        if (rangeIndicator != null)
        {
            float spriteWidth = rangeIndicator.sprite.rect.width / rangeIndicator.sprite.pixelsPerUnit;
            float spriteHeight = rangeIndicator.sprite.rect.height / rangeIndicator.sprite.pixelsPerUnit;
            float scaleFactor = targetingRange / Mathf.Max(spriteWidth, spriteHeight);
            rangeIndicator.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
        }
        towerPointingDirection = transform.rotation;
        upgradeCount = upgrade1Count + upgrade2Count;
        if (upgradeCount > 0 && towerBase.sprite != upgradedSprite)
        {
            towerBase.sprite = upgradedSprite;
        }
        if (preModAttackSpeed * ModifierScript.Instance.attackSpeedMult != attackSpeed)
        {
            ModAttackSpeed();
        }
        if (preModRange * ModifierScript.Instance.rangeMult != targetingRange)
        {
            ModRange();
        }
        if (preModDamage * ModifierScript.Instance.damageMult != damage)
        {
            ModDamage();
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
            if (CheckTargetIsInRange())
            {
                RotateTowardsTarget();
                timeUntilFire += Time.deltaTime;
                if (timeUntilFire >= 1f / attackSpeed && shotFinished)
                {
                    timeUntilFireHalf = timeUntilFire / 2;
                    Shoot();
                }
                else
                {
                    return;
                }
            }
            else
            {
                target = null;
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
            timeUntilFire = 0f;
        }
    }

    public IEnumerator ShotAnimation()
    {
        shotFinished = false;
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        bulletObj.transform.parent = transform;
        BallistaArrowScript bulletScript = bulletObj.GetComponent<BallistaArrowScript>();
        bulletScript.bulletDamage = preModDamage;
        bulletScript.bulletDamage = damage;
        attackCount++;
        damageDealt += damage;
        bulletScript.SetTarget(target);
        Debug.Log("Shoot");
        spriteRenderer.sprite = UnloadedSprite;
        shotFinished = true;
        yield return new WaitForSeconds(timeUntilFireHalf);
        spriteRenderer.sprite = LoadedSprite;
        yield return new WaitForSeconds(timeUntilFireHalf);
    }



    //private void FindTarget()
    //{
    //    RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
    //    System.Array.Sort(hits, (a, b) => a.transform.position.y.CompareTo(b.transform.position.y));
    //    System.Array.Sort(hits, (a, b) => a.transform.position.x.CompareTo(b.transform.position.x));
    //    if (hits.Length > 0)
    //    {
    //        target = hits[0].transform;
    //    }

    //}

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);
        System.Array.Sort(hits, (a, b) => {
            int compareY = a.transform.position.y.CompareTo(b.transform.position.y);
            if (compareY != 0) return compareY; // Sort by lower Y values first
                                                // If Y values are the same, sort by higher X values
            return b.transform.position.x.CompareTo(a.transform.position.x);
        });
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private void FindTargetAerial()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, flyingEnemyMask);
        System.Array.Sort(hits, (a, b) => a.transform.position.y.CompareTo(b.transform.position.y));
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    //private void RotateTowardsTarget()
    //{
    //    float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

    //    Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    //    turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed);
    //}
    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = targetRotation;
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Handles.color = Color.cyan;
    //    Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    //}

    public void ModAttackSpeed()
    {
        attackSpeed = preModAttackSpeed * ModifierScript.Instance.attackSpeedMult;
    }

    public void ModRange()
    {
        targetingRange = preModRange * ModifierScript.Instance.rangeMult;
    }
    public void ModDamage()
    {
        damage = preModDamage * ModifierScript.Instance.damageMult;
    }
}

