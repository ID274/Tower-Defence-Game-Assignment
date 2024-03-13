using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaArrowScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] public float bulletDamage;
    [SerializeField] private bool aerialBullet;

    //public float preModDamage;

    public Transform target;
    public Vector3 target2D;


    public void Start()
    {
        //preModDamage = bulletDamage;
        Destroy(gameObject, 5f);
    }

    public void Update()
    {
        if (!target)
        {
            Vector2 direction = transform.up;
            rb.velocity = direction * bulletSpeed;
            return;
        }
        //if (preModDamage * ModifierScript.Instance.damageMult != bulletDamage)
        //{
        //    ModDamage();
        //}
    }

    //private void ModDamage()
    //{
    //    bulletDamage = preModDamage * ModifierScript.Instance.damageMult;
    //}

    public void SetTarget(Transform _target)
    {
        target = _target;
    }


    private void FixedUpdate()
    {
        if (target) 
        {
            Vector2 direction = (target.position - transform.position).normalized;

            rb.velocity = direction * bulletSpeed;
            transform.up = target.position - transform.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 3)
        {
            if (other.gameObject.GetComponent<EnemyHealth>().isDestroyed == false)
            {
                other.gameObject.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
            }
        }
        if (other.gameObject.layer == 6 && aerialBullet)
        {
            if (other.gameObject.GetComponent<EnemyHealth>().isDestroyed == false)
            {
                other.gameObject.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
            }
        }
        Debug.Log("Collision");
        //Take health from an enemy
        Destroy(gameObject);
    }

}

