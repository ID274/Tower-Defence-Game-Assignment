using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI, healthUI;

    private void OnGUI()
    {
        currencyUI.text = LevelManager.main.currency.ToString();
        healthUI.text = LevelManager.main.health.ToString();
    }

    public void SetSelected()
    {

    }
}
