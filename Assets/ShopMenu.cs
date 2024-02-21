using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI, healthUI;
    [SerializeField] TextMeshProUGUI[] multText;


    private void OnGUI()
    {
        currencyUI.text = LevelManager.main.currency.ToString();
        healthUI.text = LevelManager.main.health.ToString();
    }

    private void Update()
    {
        multText[0].text = ModifierScript.Instance.goldGainMult.ToString();
        multText[1].text = ModifierScript.Instance.rangeMult.ToString();
        multText[2].text = ModifierScript.Instance.damageMult.ToString();
        multText[3].text = ModifierScript.Instance.attackSpeedMult.ToString();
    }
}
