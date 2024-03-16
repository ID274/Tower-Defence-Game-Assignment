using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UpgradeMenuScript : MonoBehaviour
{
    public static UpgradeMenuScript Instance { get; private set; }

    [Header("References")]
    public GameObject upgradeMenu;
    public TextMeshProUGUI upgradeText1;
    public TextMeshProUGUI upgradeText2;
    public TextMeshProUGUI upgradeButtonText1;
    public TextMeshProUGUI upgradeButtonText2;
    public GameObject upgradeButton1;
    public GameObject upgradeButton2;
    public TextMeshProUGUI sellButtonText;
    public GameObject iceMachineRotateButton;

    [Header("References for stat window")]
    [SerializeField] private Image towerIconSlot;
    [SerializeField] TextMeshProUGUI attackSpeedText, rangeText, damageText, attackCountText, damageDealtText;

    
    [Header("Attributes")]
    public int towerGoldWorth;
    public int upgrade1Cost;
    public int upgrade2Cost;
    public GameObject selectedTower;
    public int upgradeType; // 1 = attack speed, 2 = range, 3 = damage, 4 = slow strength, 5 = slow length

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        
    }

    void Update()
    {
        if (upgradeMenu.activeSelf)
        {

            SetStatsWindow();
            if (selectedTower.TryGetComponent(out BallistaScript ballistaTowerValues) && !ballistaTowerValues.iceMachine)
            {
                iceMachineRotateButton.SetActive(false);
                upgrade1Cost = (ballistaTowerValues.upgrade1Count + 1) * 300;
                upgrade2Cost = (ballistaTowerValues.upgrade2Count + 1) * 300;
                towerGoldWorth = ballistaTowerValues.upgradeCount * 300 + 150;
                sellButtonText.text = $"Sell for {towerGoldWorth} gold";
                Button buttonToggle1 = upgradeButton1.GetComponent<Button>();
                Button buttonToggle2 = upgradeButton2.GetComponent<Button>();
                upgradeButtonText1.text = $"Buy for {upgrade1Cost} gold";
                upgradeButtonText2.text = $"Buy for {upgrade2Cost} gold";
                upgradeText1.text = $"+{0.05f * (ballistaTowerValues.upgrade1Count + 1)} attack speed";
                upgradeText2.text = $"+{2f * (ballistaTowerValues.upgrade2Count + 1)} damage";
                if (LevelManager.main.currency < upgrade1Cost)
                {
                    buttonToggle1.interactable = false;
                }
                if (LevelManager.main.currency < upgrade2Cost)
                {
                    buttonToggle2.interactable = false;
                }
                if (LevelManager.main.currency >= upgrade1Cost)
                {
                    buttonToggle1.interactable = true;
                }
                if (LevelManager.main.currency >= upgrade2Cost)
                {
                    buttonToggle2.interactable = true;
                }
                upgradeButton1.SetActive(true);
                upgradeButton2.SetActive(true);
                ballistaTowerValues.rangeIndicator.gameObject.SetActive(true);
                //if (ballistaTowerValues.upgradePath == 0 && (!upgradeButton1.activeSelf || !upgradeButton2.activeSelf))
                //{
                //    upgradeButton1.SetActive(true);
                //    upgradeButton2.SetActive(true);
                //}
                //else if (ballistaTowerValues.upgradePath == 1 && (!upgradeButton1.activeSelf || upgradeButton2.activeSelf))
                //{
                //    upgradeButton1.SetActive(true);
                //    upgradeButton2.SetActive(false);
                //}
                //else if (ballistaTowerValues.upgradePath == 2 && (upgradeButton1.activeSelf || !upgradeButton2.activeSelf))
                //{
                //    upgradeButton1.SetActive(false);
                //    upgradeButton2.SetActive(true);
                //}
            }

            else if (selectedTower.TryGetComponent(out SpearMachineScript spearTowerValues))
            {
                iceMachineRotateButton.SetActive(false);
                upgrade1Cost = (spearTowerValues.upgrade1Count + 1) * 200;
                upgrade2Cost = (spearTowerValues.upgrade2Count + 1) * 200;
                towerGoldWorth = spearTowerValues.upgradeCount * 200 + 100;
                sellButtonText.text = $"Sell for {towerGoldWorth} gold";
                Button buttonToggle1 = upgradeButton1.GetComponent<Button>();
                Button buttonToggle2 = upgradeButton2.GetComponent<Button>();
                upgradeButtonText1.text = $"Buy for {upgrade1Cost} gold";
                upgradeButtonText2.text = $"Buy for {upgrade2Cost} gold";
                upgradeText1.text = $"+{0.15f * (spearTowerValues.upgrade1Count + 1)} attack speed";
                upgradeText2.text = $"+{0.5f * (spearTowerValues.upgrade2Count + 1)} damage";
                if (LevelManager.main.currency < upgrade1Cost)
                {
                    buttonToggle1.interactable = false;
                }
                if (LevelManager.main.currency < upgrade2Cost)
                {
                    buttonToggle2.interactable = false;
                }
                if (LevelManager.main.currency >= upgrade1Cost)
                {
                    buttonToggle1.interactable = true;
                }
                if (LevelManager.main.currency >= upgrade2Cost)
                {
                    buttonToggle2.interactable = true;
                }
                upgradeButton1.SetActive(true);
                upgradeButton2.SetActive(true);
                spearTowerValues.rangeIndicator.gameObject.SetActive(true);
                //if (spearTowerValues.upgradePath == 0 && (!upgradeButton1.activeSelf || !upgradeButton2.activeSelf))
                //{
                //    upgradeButton1.SetActive(true);
                //    upgradeButton2.SetActive(true);

                //}
                //else if (spearTowerValues.upgradePath == 1 && (!upgradeButton1.activeSelf || upgradeButton2.activeSelf))
                //{
                //    upgradeButton1.SetActive(true);
                //    upgradeButton2.SetActive(false);
                //}
                //else if (spearTowerValues.upgradePath == 2 && (upgradeButton1.activeSelf || !upgradeButton2.activeSelf))
                //{
                //    upgradeButton1.SetActive(false);
                //    upgradeButton2.SetActive(true);
                //}
            }

            else if (ballistaTowerValues.iceMachine)
            {
                iceMachineRotateButton.SetActive(true);
                upgrade1Cost = (ballistaTowerValues.upgrade1Count + 1) * 500;
                upgrade2Cost = (ballistaTowerValues.upgrade2Count + 1) * 500;
                towerGoldWorth = ballistaTowerValues.upgradeCount * 400 + 500;
                sellButtonText.text = $"Sell for {towerGoldWorth} gold";
                Button buttonToggle1 = upgradeButton1.GetComponent<Button>();
                Button buttonToggle2 = upgradeButton2.GetComponent<Button>();
                upgradeButtonText1.text = $"Buy for {upgrade1Cost} gold";
                upgradeButtonText2.text = $"Buy for {upgrade2Cost} gold";
                upgradeText1.text = $"+{0.05f * (ballistaTowerValues.upgrade1Count + 1)} slow strength";
                upgradeText2.text = $"+{0.05f * (ballistaTowerValues.upgrade2Count + 1)}s slow duration";
                if (LevelManager.main.currency < upgrade1Cost)
                {
                    buttonToggle1.interactable = false;
                }
                else if (LevelManager.main.currency >= upgrade1Cost)
                {
                    buttonToggle1.interactable = true;
                }
                if (LevelManager.main.currency < upgrade2Cost)
                {
                    buttonToggle2.interactable = false;
                }
                else if (LevelManager.main.currency >= upgrade2Cost)
                {
                    buttonToggle2.interactable = true;
                }
                upgradeButton1.SetActive(true);
                upgradeButton2.SetActive(true);
            }
        }

        

    }

    public void OnButton1Press()
    {
        if (selectedTower.TryGetComponent(out BallistaScript ballistaTowerValues) && !ballistaTowerValues.iceMachine)
        {
            upgradeType = 1;
            SetTowerUpgrades();
            LevelManager.main.currency -= upgrade1Cost;
            //Ballista upgrades should include attack speed and damage
            //ballistaTowerValues.upgradePath = 1;
            ballistaTowerValues.upgrade1Count++;
            //if (ballistaTowerValues.upgradeCount > 0)
            //{
            //    if (ballistaTowerValues.upgradePath == 1 && upgradeButton2.activeSelf)
            //    {
            //        upgradeButton2.SetActive(false);
            //    }
            //    if (ballistaTowerValues.upgradePath == 2 && upgradeButton2.activeSelf)
            //    {
            //        upgradeButton1.SetActive(false);
            //    }
            //}
        }
        else if (selectedTower.TryGetComponent(out SpearMachineScript spearTowerValues))
        {
            upgradeType = 1;
            SetTowerUpgrades();
            LevelManager.main.currency -= upgrade1Cost;
            //Spear Machine upgrades should include attack speed and damage
            //spearTowerValues.upgradePath = 1;
            spearTowerValues.upgrade1Count++;
            //if (spearTowerValues.upgradeCount > 0)
            //{
            //    if (spearTowerValues.upgradePath == 1 && upgradeButton2.activeSelf)
            //    {
            //        upgradeButton2.SetActive(false);
            //    }
            //    if (spearTowerValues.upgradePath == 2 && upgradeButton2.activeSelf)
            //    {
            //        upgradeButton1.SetActive(false);
            //    }
            //}
        }
        else if (ballistaTowerValues.iceMachine)
        {
            upgradeType = 4;
            SetTowerUpgrades();
            LevelManager.main.currency -= upgrade1Cost;
            ballistaTowerValues.upgrade1Count++;
        }
    }
    public void OnButton2Press()
    {
        if (selectedTower.TryGetComponent(out BallistaScript ballistaTowerValues) && !ballistaTowerValues.iceMachine)
        {
            upgradeType = 3;
            SetTowerUpgrades();
            LevelManager.main.currency -= upgrade2Cost;
            //ballistaTowerValues.upgradePath = 2;
            ballistaTowerValues.upgrade2Count++;
            //if (ballistaTowerValues.upgradeCount > 0)
            //{
            //    if (ballistaTowerValues.upgradePath == 1 && upgradeButton2.activeSelf)
            //    {
            //        upgradeButton2.SetActive(false);
            //    }
            //    if (ballistaTowerValues.upgradePath == 2 && upgradeButton2.activeSelf)
            //    {
            //        upgradeButton1.SetActive(false);
            //    }
            //}
        }
        else if (selectedTower.TryGetComponent(out SpearMachineScript spearTowerValues))
        {
            upgradeType = 3;
            SetTowerUpgrades();
            LevelManager.main.currency -= upgrade2Cost;
            //spearTowerValues.upgradePath = 2;
            spearTowerValues.upgrade2Count++;
            //if (spearTowerValues.upgradeCount > 0)
            //{
            //    if (spearTowerValues.upgradePath == 1 && upgradeButton2.activeSelf)
            //    {
            //        upgradeButton2.SetActive(false);
            //    }
            //    if (spearTowerValues.upgradePath == 2 && upgradeButton2.activeSelf)
            //    {
            //        upgradeButton1.SetActive(false);
            //    }
            //}
        }
        else if (ballistaTowerValues.iceMachine)
        {
            upgradeType = 5;
            SetTowerUpgrades();
            LevelManager.main.currency -= upgrade2Cost;
            ballistaTowerValues.upgrade2Count++;
        }
    }

    public void OnCloseButtonPress()
    {
        if (selectedTower.TryGetComponent(out BallistaScript ballistaTowerValues) && !ballistaTowerValues.iceMachine)
        {
            ballistaTowerValues.rangeIndicator.gameObject.SetActive(false);
        }
        if (selectedTower.TryGetComponent(out SpearMachineScript spearTowerValues))
        {
            spearTowerValues.rangeIndicator.gameObject.SetActive(false);
        }
        upgradeMenu.SetActive(false);
    }

    public void SetTowerUpgrades()
    {
        if (selectedTower.TryGetComponent(out BallistaScript ballistaTowerValues))
        {
            switch (upgradeType)
            {
                case 1:
                    ballistaTowerValues.preModAttackSpeed += 0.05f * (ballistaTowerValues.upgrade1Count + 1);
                    //ballistaTowerValues.ModAttackSpeed();
                    break;
                case 3:
                    ballistaTowerValues.preModDamage += 2f * (ballistaTowerValues.upgrade2Count + 1);
                    //ballistaTowerValues.ModDamage();
                    break;
                case 4:
                    ballistaTowerValues.slowStrength += 0.05f * (ballistaTowerValues.upgrade1Count + 1);
                    break;
                case 5:
                    ballistaTowerValues.slowLength += 0.05f * (ballistaTowerValues.upgrade2Count + 1);
                    break;
            }
        }
        else if (selectedTower.TryGetComponent(out SpearMachineScript spearTowerValues))
        {
            switch (upgradeType)
            {
                case 1:
                    spearTowerValues.preModAttackSpeed += 0.15f * (spearTowerValues.upgrade1Count + 1);
                    //spearTowerValues.ModAttackSpeed();
                    break;
                case 3:
                    spearTowerValues.preModDamage += 0.5f * (spearTowerValues.upgrade2Count + 1);
                    //spearTowerValues.ModDamage();
                    break;
            }
        }
    }

    public void SetStatsWindow()
    {
        if (selectedTower.TryGetComponent(out BallistaScript ballistaTowerValues))
        {
            if (!ballistaTowerValues.cannon && !ballistaTowerValues.iceMachine)
            {
                towerIconSlot.sprite = ballistaTowerValues.LoadedSprite;
            }
            else if (!ballistaTowerValues.iceMachine)
            {
                towerIconSlot.sprite = ballistaTowerValues.cannonSprite[0];
            }
            if (ballistaTowerValues.aerial && !ballistaTowerValues.iceMachine)
            {
                towerIconSlot.color = new Color32(66, 191, 255, 255);
            }
            else
            {
                towerIconSlot.color = Color.white;
            }
            if (!ballistaTowerValues.iceMachine)
            {
                attackCountText.text = $"Attacked: {ballistaTowerValues.attackCount} times";
                attackSpeedText.text = $"Attack speed: {ballistaTowerValues.attackSpeed.ToString("F2")}/s";
                rangeText.text = $"Range: {ballistaTowerValues.targetingRange.ToString("F2")} units";
                damageText.text = $"Damage: {ballistaTowerValues.damage.ToString("F2")}";
                damageDealtText.text = $"Damage dealt: {ballistaTowerValues.damageDealt.ToString("F2")}";
            }
            else
            {
                towerIconSlot.sprite = ballistaTowerValues.spriteRenderer.sprite;
                attackCountText.text = $"Slow strength is the value that enemy movement speed is divided by.";
                attackSpeedText.text = $"Slow duration: {ballistaTowerValues.slowLength.ToString("F2")}s";
                rangeText.text = $"Slow strength: {ballistaTowerValues.slowStrength.ToString("F2")}";
                damageText.text = $"Enemies slowed: {ballistaTowerValues.enemiesSlowed}";
                damageDealtText.text = $"";
            }

        }
        if (selectedTower.TryGetComponent(out SpearMachineScript spearTowerValues))
        {
            towerIconSlot.color = Color.white;
            towerIconSlot.sprite = spearTowerValues.spriteCentered;
            attackCountText.text = $"Attacked: {spearTowerValues.attackCount} times";
            attackSpeedText.text = $"Attack speed: {spearTowerValues.attackSpeed.ToString("F2")}/s";
            rangeText.text = $"Range: {spearTowerValues.targetingRange.ToString("F2")} units";
            damageText.text = $"Damage: {spearTowerValues.damage.ToString("F2")}";
            damageDealtText.text = $"Damage dealt: {spearTowerValues.damageDealt.ToString("F2")}";
        }
    }


    public void SellTower()
    {
        Destroy(selectedTower);
        LevelManager.main.currency += towerGoldWorth;
        upgradeMenu.SetActive(false);
        
    }

    public void RotateIceMachine()
    {
        if (selectedTower.TryGetComponent(out BallistaScript ballistaTowerValues) && ballistaTowerValues.iceMachine)
        {
            if (ballistaTowerValues.directionIndex < 4)
            {
                ballistaTowerValues.directionIndex++;
            }
            else
            {
                ballistaTowerValues.directionIndex = 1;
            }
        }
    }
}
