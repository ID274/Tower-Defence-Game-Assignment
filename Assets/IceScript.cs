using System.Collections;
using System.Collections.Generic;
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 3 || other.gameObject.layer == 6)
        {
            iceMachineScript.enemiesSlowed++;
            Debug.Log("Ice collision");
            EnemyMovement enemyMovementInstance = other.gameObject.GetComponent<EnemyMovement>();
            StartCoroutine(Slow(enemyMovementInstance));
        }
    }

    private IEnumerator Slow(EnemyMovement enemy)
    {
        float originalMoveSpeed = enemy.moveSpeed;
        enemy.moveSpeed /= iceMachineScript.slowStrength;
        yield return new WaitForSeconds(6f);
        enemy.moveSpeed = originalMoveSpeed;
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
