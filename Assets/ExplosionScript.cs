using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] private BallistaArrowScript bulletScript;

    private void Start()
    {
        float transformFromOne = -1 + transform.localScale.z;
        transform.localScale -= new Vector3(0, 0, transformFromOne);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 3)
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(bulletScript.bulletDamage);
        }
    }
}
