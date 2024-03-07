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
    //[SerializeField] private GameObject bulletPrefab;
    //[SerializeField] private Transform firingPoint;

    [Header("Attributes for stats window")]
    public int attackCount;
    public float damageDealt;

    [Header("Attributes")]
    public float damage = 3;
    public float targetingRange = 2f;
    [SerializeField] private float rotationSpeed = 10f;
    public float attackSpeed = 1f; //hits per second
    public int upgradePath = 0;
    public int upgradeCount = 0;

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
                Shoot();
            }
        }
    }

    private void Shoot()
    {

        if (shotFinished)
        {
            //StartCoroutine(ShotAnimation());
            StartCoroutine(AttackAnimation());
            timeUntilFire = 0f;
        }
        else
        {
            return;
        }
    }


    public IEnumerator AttackAnimation()
    {
        shotFinished = false;
        timeUntilFireQuarter = timeUntilFire / 4;
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
        attackCount++;
        damageDealt += damage;
    }
    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
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
