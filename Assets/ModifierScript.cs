using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierScript : MonoBehaviour
{
    public static ModifierScript Instance { get; private set; }
    public float goldGainMult;
    public float attackSpeedMult;
    public float damageMult;
    public float rangeMult;


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

    private void Update()
    {
        if (goldGainMult < 1 || attackSpeedMult < 1 || rangeMult < 1 || damageMult < 1)
        {
            Debug.LogError($"Error: A multiplier in ModifierScript.cs is below 1. Setting values to 1.\nClick to expand the error message.\nAttack Speed Multiplier: {attackSpeedMult}\nGold Gain Multiplier: {goldGainMult}\nRange Multiplier: {rangeMult}\nDamage Multiplier: {damageMult}");
            goldGainMult = 1;
            attackSpeedMult = 1;
            rangeMult = 1;
            damageMult = 1;
}
    }
}
