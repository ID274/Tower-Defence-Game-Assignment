using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.TextCore.Text;
using Unity.VisualScripting;

public class PowerupScript : MonoBehaviour
{
    [SerializeField] private int tier; //rarity between common, rare, epic, legendary
    [SerializeField] private int value; //the amount displayed
    [SerializeField] private float actualValue; //the amount that gets added to the multiplier
    [SerializeField] private int upgrade; //1 = gold multiplier, 2 = range multiplier, 3 = damage multiplier, 4 = attack speed multiplier
    [SerializeField] private string upgradeName; //Gold Multiplier etc
    [SerializeField] private string tierName; //Common, Rare etc
    [SerializeField] private TextMeshProUGUI[] text;
    [SerializeField] private Color[] tierColor;

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
                    upgradeName = "Gold Multiplier";
                    break;
                }
            case 2:
                {
                    upgradeName = "Range Multiplier";
                    break;
                }
            case 3:
                {
                    upgradeName = "Damage Multiplier";
                    break;
                }
            case 4:
                {
                    upgradeName = "A. Speed Multiplier";
                    break;
                }
        }

        if (tier <= 35)
        {
            tierName = "Common";
            textColor = tierColor[0];
            value = 5; //percent
            actualValue = 0.05f;
        }
        else if (tier > 35 && tier <= 70)
        {
            tierName = "Rare";
            textColor = tierColor[1];
            value = 10;
            actualValue = 0.1f;
        }
        else if (tier > 70 && tier <= 90)
        {
            tierName = "Epic";
            textColor = tierColor[2];
            value = 15;
            actualValue = 0.15f;
        }
        else if (tier > 90)
        {
            tierName = "Legendary";
            textColor = tierColor[3];
            value = 25;
            actualValue = 0.15f;
        }


        text[0].text = upgradeName;
        text[1].text = tierName;
        text[2].text = $"+{value}%";

        for (int i = 0; i < text.Length; i++)
        {
            text[i].color = textColor;
            
        }
    }





}
