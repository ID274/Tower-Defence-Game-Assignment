using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    
    [Header("Attributes")]
    public int towerGoldWorth;
    public int upgrade1Cost;
    public int upgrade2Cost;
    public GameObject selectedTower;

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
            if (selectedTower.TryGetComponent(out BallistaScript ballistaTowerValues))
            {
                if (ballistaTowerValues.upgradePath == 0 && (!upgradeButton1.activeSelf || !upgradeButton2.activeSelf))
                {
                    upgradeButton1.SetActive(true);
                    upgradeButton2.SetActive(true);
                }
                else if (ballistaTowerValues.upgradePath == 1 && (!upgradeButton1.activeSelf || upgradeButton2.activeSelf))
                {
                    upgradeButton1.SetActive(true);
                    upgradeButton2.SetActive(false);
                }
                else if (ballistaTowerValues.upgradePath == 2 && (upgradeButton1.activeSelf || !upgradeButton2.activeSelf))
                {
                    upgradeButton1.SetActive(false);
                    upgradeButton2.SetActive(true);
                }
            }
            else if (selectedTower.TryGetComponent(out SpearMachineScript spearTowerValues))
            {
                if (spearTowerValues.upgradePath == 0 && (!upgradeButton1.activeSelf || !upgradeButton2.activeSelf))
                {
                    upgradeButton1.SetActive(true);
                    upgradeButton2.SetActive(true);
                }
                else if (spearTowerValues.upgradePath == 1 && (!upgradeButton1.activeSelf || upgradeButton2.activeSelf))
                {
                    upgradeButton1.SetActive(true);
                    upgradeButton2.SetActive(false);
                }
                else if (spearTowerValues.upgradePath == 2 && (upgradeButton1.activeSelf || !upgradeButton2.activeSelf))
                {
                    upgradeButton1.SetActive(false);
                    upgradeButton2.SetActive(true);
                }
            }
        }

        

    }

    public void OnButton1Press()
    {
        if (selectedTower.TryGetComponent(out BallistaScript ballistaTowerValues))
        {
            //Ballista upgrades should include attack speed and range
            ballistaTowerValues.upgradePath = 1;
            ballistaTowerValues.upgradeCount++;
            if (ballistaTowerValues.upgradeCount > 0)
            {
                if (ballistaTowerValues.upgradePath == 1 && upgradeButton2.activeSelf)
                {
                    upgradeButton2.SetActive(false);
                }
                if (ballistaTowerValues.upgradePath == 2 && upgradeButton2.activeSelf)
                {
                    upgradeButton1.SetActive(false);
                }
            }
        }
        else if (selectedTower.TryGetComponent(out SpearMachineScript spearTowerValues))
        {
            //Spear Machine upgrades should include attack speed and damage
            spearTowerValues.upgradePath = 1;
            spearTowerValues.upgradeCount++;
            if (spearTowerValues.upgradeCount > 0)
            {
                if (spearTowerValues.upgradePath == 1 && upgradeButton2.activeSelf)
                {
                    upgradeButton2.SetActive(false);
                }
                if (spearTowerValues.upgradePath == 2 && upgradeButton2.activeSelf)
                {
                    upgradeButton1.SetActive(false);
                }
            }
        }
    }
    public void OnButton2Press()
    {
        if (selectedTower.TryGetComponent(out BallistaScript ballistaTowerValues))
        {
            ballistaTowerValues.upgradePath = 2;
            ballistaTowerValues.upgradeCount++;
            if (ballistaTowerValues.upgradeCount > 0)
            {
                if (ballistaTowerValues.upgradePath == 1 && upgradeButton2.activeSelf)
                {
                    upgradeButton2.SetActive(false);
                }
                if (ballistaTowerValues.upgradePath == 2 && upgradeButton2.activeSelf)
                {
                    upgradeButton1.SetActive(false);
                }
            }
        }
        else if (selectedTower.TryGetComponent(out SpearMachineScript spearTowerValues))
        {
            spearTowerValues.upgradePath = 2;
            spearTowerValues.upgradeCount++;
            if (spearTowerValues.upgradeCount > 0)
            {
                if (spearTowerValues.upgradePath == 1 && upgradeButton2.activeSelf)
                {
                    upgradeButton2.SetActive(false);
                }
                if (spearTowerValues.upgradePath == 2 && upgradeButton2.activeSelf)
                {
                    upgradeButton1.SetActive(false);
                }
            }
        }
    }

    public void OnCloseButtonPress()
    {
        upgradeMenu.SetActive(false);
    }
}
