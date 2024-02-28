using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.TextCore.Text;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class PowerupScript : MonoBehaviour
{
    [SerializeField] private int tier; //rarity between common, rare, epic, legendary
    [SerializeField] private int value; //the amount displayed
    [SerializeField] private float actualValue; //the amount that gets added to the multiplier
    [SerializeField] private int upgrade; //1 = gold multiplier, 2 = range multiplier, 3 = damage multiplier, 4 = attack speed multiplier
    [SerializeField] private string upgradeName; //Gold Multiplier etc
    [SerializeField] private string tierName; //Common, Rare etc
    [SerializeField] private GameObject iconObj; // Object that holds the image component for the icon
    [SerializeField] private Sprite[] image; // Sprites for the icons
    [SerializeField] private Sprite icon; // The sprite picked
    [SerializeField] private TextMeshProUGUI[] text; // Text components
    [SerializeField] private Color[] tierColor; // Color depending on "rarity" or tier of the upgrade

    public GameObject powerupScreen;


    private Color textColor;

    void OnEnable()
    {
        RollUpgrades();
    }

    void RollUpgrades()
    {
        upgrade = Random.Range(1, 5);
        tier = Random.Range(1, 101);
        switch (upgrade)
        {
            case 1:
                {
                    icon = image[0];
                    iconObj.GetComponent<Image>().sprite = icon;
                    upgradeName = "Gold Multiplier";
                    break;
                }
            case 2:
                {
                    icon = image[1];
                    iconObj.GetComponent<Image>().sprite = icon;
                    upgradeName = "Range Multiplier";
                    break;
                }
            case 3:
                {
                    icon = image[2];
                    iconObj.GetComponent<Image>().sprite = icon;
                    upgradeName = "Damage Multiplier";
                    break;
                }
            case 4:
                {
                    icon = image[3];
                    iconObj.GetComponent<Image>().sprite = icon;
                    upgradeName = "A. Speed Multiplier";
                    break;
                }
        }

        if (tier <= 50)
        {
            tierName = "Common";
            textColor = tierColor[0];
            value = 5; //percent
            actualValue = 0.05f;
            this.GetComponent<Image>().color = tierColor[0];
        }
        else if (tier > 50 && tier <= 80)
        {
            tierName = "Rare";
            textColor = tierColor[1];
            value = 10;
            actualValue = 0.1f;
            this.GetComponent<Image>().color = tierColor[1];
        }
        else if (tier > 80 && tier <= 95)
        {
            tierName = "Epic";
            textColor = tierColor[2];
            value = 15;
            actualValue = 0.15f;
            this.GetComponent<Image>().color = tierColor[2];
        }
        else if (tier > 95)
        {
            tierName = "Legendary";
            textColor = tierColor[3];
            value = 25;
            actualValue = 0.25f;
            this.GetComponent<Image>().color = tierColor[3];
        }


        text[0].text = upgradeName;
        text[1].text = tierName;
        text[2].text = $"+{value}%";


        for (int i = 0; i < text.Length; i++)
        {
            text[i].color = textColor;
            
        }


    }

    public void ButtonClicked()
    {
        if (upgradeName == "Gold Multiplier")
        {
            ModifierScript.Instance.goldGainMult += actualValue;
        }
        if (upgradeName == "Range Multiplier")
        {
            ModifierScript.Instance.rangeMult += actualValue;
        }
        if (upgradeName == "Damage Multiplier")
        {
            ModifierScript.Instance.damageMult += actualValue;
        }
        if (upgradeName == "A. Speed Multiplier")
        {
            ModifierScript.Instance.attackSpeedMult += actualValue;
        }
        powerupScreen.SetActive(false);
    }



}
