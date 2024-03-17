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

    public Sprite[] cannonSprite;

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
    private float timeUntilFireTwelth;
    public bool aerial;
    public bool cannon;
    private bool firstAttack = true;

    [Header("Ice Machine")]
    public bool iceMachine;
    [SerializeField] private GameObject firingTop, firingRight, firingBot, firingLeft;
    [SerializeField] public int directionIndex = 1;
    [SerializeField] public float slowStrength;
    [SerializeField] public float slowLength;
    [SerializeField] public int enemiesSlowed;

    public Quaternion towerPointingDirection;



    private void Start()
    {
        preModDamage = damage;
        preModAttackSpeed = attackSpeed;
        preModRange = targetingRange;
    }


    private void Update()
    {
        if (iceMachine)
        {
            upgradeCount = upgrade1Count;
            if (upgradeCount > 0 && towerBase.sprite != upgradedSprite)
            {
                towerBase.sprite = upgradedSprite;
            }
            switch (directionIndex)
            {
                case 1:
                    firingTop.SetActive(true);
                    firingRight.SetActive(false);
                    firingBot.SetActive(false);
                    firingLeft.SetActive(false);
                    break;
                case 2:
                    firingTop.SetActive(false);
                    firingRight.SetActive(true);
                    firingBot.SetActive(false);
                    firingLeft.SetActive(false);
                    break;
                case 3:
                    firingTop.SetActive(false);
                    firingRight.SetActive(false);
                    firingBot.SetActive(true);
                    firingLeft.SetActive(false);
                    break;
                case 4:
                    firingTop.SetActive(false);
                    firingRight.SetActive(false);
                    firingBot.SetActive(false);
                    firingLeft.SetActive(true);
                    break;
            }
        }
        else
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
            if (!LevelManager.Instance.gameOver)
            {
                if (target == null || !CheckTargetIsInRange())
                {
                    FindTargetAerial();
                    if (target == null || !CheckTargetIsInRange())
                    {
                        FindTarget();
                    }
                    return;
                }
                else
                {
                    if (firstAttack)
                    {
                        firstAttack = false;
                        RotateTowardsTarget();
                        Shoot();
                    }
                    else
                    {
                        RotateTowardsTarget();
                        timeUntilFire += Time.deltaTime;
                        if (timeUntilFire >= 1f / attackSpeed && shotFinished)
                        {
                            timeUntilFireHalf = timeUntilFire / 2;
                            timeUntilFireTwelth = timeUntilFire / 12;
                            Shoot();
                        }
                    }
                }
            }
        }
        
    }

    private void Shoot()
    {
        if (shotFinished)
        {
            if (cannon)
            {
                StartCoroutine(CannonAnimation());
            }
            else
            {
                StartCoroutine(ShotAnimation());
            }
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
        if (target != null)
        {
            bulletScript.SetTarget(target);
        }
        Debug.Log("Shoot");
        spriteRenderer.sprite = UnloadedSprite;
        shotFinished = true;
        yield return new WaitForSeconds(timeUntilFireHalf);
        spriteRenderer.sprite = LoadedSprite;
        yield return new WaitForSeconds(timeUntilFireHalf);
    }

    public IEnumerator CannonAnimation()
    {
        shotFinished = false;
        spriteRenderer.sprite = cannonSprite[0];
        yield return new WaitForSeconds(timeUntilFireTwelth);
        spriteRenderer.sprite = cannonSprite[1];
        yield return new WaitForSeconds(timeUntilFireTwelth);
        spriteRenderer.sprite = cannonSprite[2];
        yield return new WaitForSeconds(timeUntilFireTwelth);
        spriteRenderer.sprite = cannonSprite[3];
        yield return new WaitForSeconds(timeUntilFireTwelth);
        spriteRenderer.sprite = cannonSprite[4];
        yield return new WaitForSeconds(timeUntilFireTwelth);
        spriteRenderer.sprite = cannonSprite[5];
        yield return new WaitForSeconds(timeUntilFireTwelth);
        spriteRenderer.sprite = cannonSprite[6];
        yield return new WaitForSeconds(timeUntilFireTwelth);
        spriteRenderer.sprite = cannonSprite[7];
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        bulletObj.transform.parent = transform;
        BallistaArrowScript bulletScript = bulletObj.GetComponent<BallistaArrowScript>();
        bulletScript.bulletDamage = preModDamage;
        bulletScript.bulletDamage = damage;
        attackCount++;
        damageDealt += damage;
        bulletScript.SetTarget(target);
        Debug.Log("Shoot");
        yield return new WaitForSeconds(timeUntilFireTwelth);
        spriteRenderer.sprite = cannonSprite[8];
        yield return new WaitForSeconds(timeUntilFireTwelth);
        spriteRenderer.sprite = cannonSprite[9];
        yield return new WaitForSeconds(timeUntilFireTwelth);
        spriteRenderer.sprite = cannonSprite[10];
        yield return new WaitForSeconds(timeUntilFireTwelth);
        spriteRenderer.sprite = cannonSprite[11];
        yield return new WaitForSeconds(timeUntilFireTwelth);
        spriteRenderer.sprite = cannonSprite[0];
        shotFinished = true;
    }
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
        return target != null && Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = targetRotation;
    }
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

