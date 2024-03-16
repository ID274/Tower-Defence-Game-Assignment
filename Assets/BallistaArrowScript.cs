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
    [SerializeField] private bool cannonBall;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject cannonBullet;
    [SerializeField] private GameObject turretRotationPoint;
    private bool exploding = false;

    //public float preModDamage;

    public Transform target;
    public Vector3 target2D;


    public void Start()
    {
        turretRotationPoint = transform.parent.Find("RotatePoint").gameObject;
        //preModDamage = bulletDamage;
        Destroy(gameObject, 5f);
    }

    public void Update()
    {
        if (!target)
        {
            if (exploding)
            {
                rb.velocity = new Vector2(0f, 0f);
            }
            else
            {
                Vector2 direction = turretRotationPoint.transform.up; ;
                rb.velocity = direction * bulletSpeed;
                return;
            }
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
            if (exploding)
            {
                rb.velocity = new Vector2(0f, 0f);
            }
            else
            {
                Vector2 direction = (target.position - transform.position).normalized;

                rb.velocity = direction * bulletSpeed;
                transform.up = target.position - transform.position;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 3)
        {
            if (!cannonBall)
            {
                if (other.gameObject.GetComponent<EnemyHealth>().isDestroyed == false)
                {
                    other.gameObject.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
                }
                Destroy(gameObject);
            }
            else
            {
                cannonBullet.SetActive(false);
                if (!exploding)
                {
                    StartCoroutine(Explode());
                }
            }

        }
        if (other.gameObject.layer == 6 && aerialBullet)
        {
            if (other.gameObject.GetComponent<EnemyHealth>().isDestroyed == false)
            {
                other.gameObject.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
            }
            Destroy(gameObject);
        }
        Debug.Log("Collision");
        //Take health from an enemy
        
    }

    private IEnumerator Explode()
    {
        exploding = true;
        explosion.SetActive(true);
        explosion.transform.localScale = new Vector2(0.5f, 0.5f);
        SpriteRenderer explosionSprite = explosion.GetComponent<SpriteRenderer>();
        yield return new WaitForSeconds(0.1f);
        explosion.transform.localScale = new Vector2(0.7f, 0.7f);
        explosionSprite.color = new Color(explosionSprite.color.g, explosionSprite.color.b, explosionSprite.color.a, 1f);
        yield return new WaitForSeconds(0.1f);
        explosion.transform.localScale = new Vector2(0.9f, 0.9f);
        explosionSprite.color = new Color(explosionSprite.color.g, explosionSprite.color.b, explosionSprite.color.a, 0.8f);
        yield return new WaitForSeconds(0.1f);
        explosion.transform.localScale = new Vector2(1.1f, 1.1f);
        explosionSprite.color = new Color(explosionSprite.color.g, explosionSprite.color.b, explosionSprite.color.a, 0.6f);
        yield return new WaitForSeconds(0.1f);
        explosion.transform.localScale = new Vector2(1.3f, 1.3f);
        explosionSprite.color = new Color(explosionSprite.color.g, explosionSprite.color.b, explosionSprite.color.a, 0.4f);
        yield return new WaitForSeconds(0.1f);
        explosion.transform.localScale = new Vector2(1.5f, 1.5f);
        explosionSprite.color = new Color(explosionSprite.color.g, explosionSprite.color.b, explosionSprite.color.a, 0.2f);
        Destroy(gameObject);
    }

}

