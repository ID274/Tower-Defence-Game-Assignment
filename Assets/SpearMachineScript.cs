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
    [SerializeField] private bool shotFinished = true;

    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    //[SerializeField] private GameObject bulletPrefab;
    //[SerializeField] private Transform firingPoint;

    [Header("Attributes")]
    [SerializeField] private int damage = 3;
    [SerializeField] private float targetingRange = 2f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float attackSpeed = 1f; //hits per second

    private Transform target;
    private float timeUntilFire;
    private float timeUntilFireQuarter;

    private float preModAttackSpeed;
    private float preModRange;
    private float preModDamage;



    private void Start()
    {
        preModAttackSpeed = attackSpeed;
        preModRange = targetingRange;
        preModDamage = damage;
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
        if (preModDamage * ModifierScript.Instance.damageMult != damage)
        {
            ModRange();
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
    }
    //public IEnumerator ShotAnimation()
    //{
    //    timeUntilFireHalf = timeUntilFire / 2;
    //    GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
    //    BallistaArrowScript bulletScript = bulletObj.GetComponent<BallistaArrowScript>();
    //    bulletScript.SetTarget(target);
    //    Debug.Log("Attack");
    //    shotFinished = false;
    //    spriteRenderer.sprite = UnloadedSprite;
    //    yield return new WaitForSeconds(timeUntilFireHalf);
    //    spriteRenderer.sprite = LoadedSprite;
    //    yield return new WaitForSeconds(timeUntilFireHalf);
    //    shotFinished = true;
    //}

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

    void ModAttackSpeed()
    {
        attackSpeed = preModAttackSpeed * ModifierScript.Instance.attackSpeedMult;
    }

    void ModRange()
    {
        targetingRange = preModRange * ModifierScript.Instance.rangeMult;
    }

    void ModDamage()
    {
        damage = Mathf.RoundToInt(preModDamage * ModifierScript.Instance.damageMult);
    }

}
