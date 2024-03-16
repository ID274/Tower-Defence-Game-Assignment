using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAnimationScript : MonoBehaviour
{
    [SerializeField] Animator animator;

    void Start()
    {
        animator.SetBool("ShopOpen", false);
    }


    public void ToggleShop()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            if (animator.GetBool("ShopOpen"))
            {
                animator.SetBool("ShopOpen", false);
            }
            else
            {
                animator.SetBool("ShopOpen", true);
            }
        }
    }
}
