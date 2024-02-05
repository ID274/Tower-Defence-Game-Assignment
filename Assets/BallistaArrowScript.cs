using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaArrowScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private int bulletDamage = 1;

    public Transform target;
    public Vector3 target2D;


    public void Start()
    {
        Destroy(gameObject, 5f);
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }


    private void FixedUpdate()
    {
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;
        transform.up = target.position - transform.position;
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
        {

        }
        Debug.Log("Collision");
        //Take health from an enemy
        Destroy(gameObject);
    }

}

