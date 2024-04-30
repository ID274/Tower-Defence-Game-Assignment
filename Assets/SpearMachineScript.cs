using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class SpearMachineScript : MonoBehaviour
{
    //Animation
    public SpriteRenderer spriteRenderer;
    public Sprite sprite1, sprite2, sprite3, sprite4;
    public Sprite spriteCentered;
    [SerializeField] private bool shotFinished = true;

    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private SpriteRenderer towerBase;
    [SerializeField] private Sprite upgradedSprite;
    [SerializeField] public SpriteRenderer rangeIndicator;
    //[SerializeField] private GameObject bulletPrefab;
    //[SerializeField] private Transform firingPoint;

    [Header("Attributes for stats window")]
    public int attackCount;
    public float damageDealt;

    [Header("Attributes")]
    public float damage = 3;
    public float targetingRange = 2f;
    //[SerializeField] private float rotationSpeed = 10f;
    public float attackSpeed = 1f; //hits per second
    public int upgradePath = 0;
    public int upgrade1Count = 0;
    public int upgrade2Count = 0;
    public int upgradeCount;

    private Transform target;
    private float timeUntilFire;
    private float timeUntilFireQuarter;

    public float preModAttackSpeed;
    private float preModRange;
    public float preModDamage;



    private void Start()
    {
        preModAttackSpeed = attackSpeed;
        preModRange = targetingRange;
        preModDamage = damage;
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

        if (target == null)
        {
            FindTarget();
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
                timeUntilFireQuarter = timeUntilFire / 4;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        if (shotFinished)
        {
            StartCoroutine(AttackAnimation());
            timeUntilFire = 0f;
        }
        else
        {
            timeUntilFire = 0f;
        }
    }


    public IEnumerator AttackAnimation()
    {
        shotFinished = false;
        spriteRenderer.sprite = sprite4;
        Attack();
        yield return new WaitForSeconds(timeUntilFireQuarter / 2);
        spriteRenderer.sprite = sprite3;
        yield return new WaitForSeconds(timeUntilFireQuarter / 2);
        spriteRenderer.sprite = sprite2;
        yield return new WaitForSeconds(timeUntilFireQuarter / 2);
        spriteRenderer.sprite = sprite1;
        yield return new WaitForSeconds(timeUntilFireQuarter / 2 + timeUntilFireQuarter * 2);
        shotFinished = true;

    }

    public void Attack()
    {
        target.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        SoundManager.Instance.PlaySFX(SoundManager.Instance.spearSFX);
        attackCount++;
        damageDealt += damage;
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
