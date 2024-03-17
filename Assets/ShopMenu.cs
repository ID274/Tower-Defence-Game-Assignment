using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI, healthUI, waveUI;
    [SerializeField] TextMeshProUGUI[] multText;

    public GameObject settingsMenu;


    private void OnGUI()
    {
        currencyUI.text = LevelManager.Instance.currency.ToString();
        healthUI.text = LevelManager.Instance.health.ToString();
        waveUI.text = LevelManager.Instance.currentWave.ToString();
    }

    private void Update()
    {
        multText[0].text = ModifierScript.Instance.goldGainMult.ToString();
        multText[1].text = ModifierScript.Instance.rangeMult.ToString();
        multText[2].text = ModifierScript.Instance.damageMult.ToString();
        multText[3].text = ModifierScript.Instance.attackSpeedMult.ToString();
    }
    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
    }
}

