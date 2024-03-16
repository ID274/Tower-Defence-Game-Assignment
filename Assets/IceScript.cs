using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IceScript : MonoBehaviour
{
    [SerializeField] private BallistaScript iceMachineScript;
    [SerializeField] private Sprite anim0, anim1, anim2, anim3, anim4, anim5;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        StartCoroutine(Animation());
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if ((other.gameObject.layer == 3 || other.gameObject.layer == 6))
        {
            if (other.GetComponent<EnemyMovement>().slowed == false)
            {
                iceMachineScript.enemiesSlowed++;
                Debug.Log("Ice collision");
                EnemyMovement enemyMovementInstance = other.gameObject.GetComponent<EnemyMovement>();
                SlowDuringCollision(enemyMovementInstance);
            }
            else if (other.GetComponent<EnemyMovement>() && other.GetComponent<EnemyMovement>().slowedByGameObject != gameObject)
            {
                EnemyMovement enemyMovementInstance = other.gameObject.GetComponent<EnemyMovement>();

                if (enemyMovementInstance.moveSpeed > enemyMovementInstance.originalMoveSpeed / gameObject.transform.parent.transform.parent.GetComponent<BallistaScript>().slowStrength)
                {
                    UnSlowAfterCollision(enemyMovementInstance);
                    SlowDuringCollision(enemyMovementInstance);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 3 || other.gameObject.layer == 6)
        {
            if (other.TryGetComponent(out EnemyMovement enemyMovementInstance))
            {
                if (enemyMovementInstance.slowedByGameObject == gameObject)
                {
                    enemyMovementInstance.moveSpeed = enemyMovementInstance.originalMoveSpeed;
                    enemyMovementInstance.slowed = false;
                    StartCoroutine(Slow(enemyMovementInstance));
                }
                else
                {
                    return;
                }
            }            
        }
    }

    private IEnumerator Slow(EnemyMovement enemy)
    {
        enemy.slowedByGameObject = gameObject;
        enemy.slowed = true;
        enemy.moveSpeed /= iceMachineScript.slowStrength;
        yield return new WaitForSeconds(iceMachineScript.slowLength);
        UnSlowAfterCollision(enemy);
    }
    private void SlowDuringCollision(EnemyMovement enemy)
    {
        enemy.slowedByGameObject = gameObject;
        enemy.slowed = true;
        enemy.moveSpeed /= iceMachineScript.slowStrength;
    }
    private void UnSlowAfterCollision(EnemyMovement enemy)
    {
        enemy.slowedByGameObject = null;
        enemy.slowed = false;
        enemy.moveSpeed = enemy.originalMoveSpeed;
    }

    private IEnumerator Animation()
    {
        spriteRenderer.sprite = anim0;
        yield return new WaitForSecondsRealtime(0.1f);
        spriteRenderer.sprite = anim1;
        yield return new WaitForSecondsRealtime(0.1f);
        spriteRenderer.sprite = anim2;
        yield return new WaitForSecondsRealtime(0.1f);
        spriteRenderer.sprite = anim3;
        yield return new WaitForSecondsRealtime(0.1f);
        spriteRenderer.sprite = anim4;
        yield return new WaitForSecondsRealtime(0.1f);
        spriteRenderer.sprite = anim5;
        yield return new WaitForSecondsRealtime(0.1f);
        StartCoroutine(Animation());
    }
}
