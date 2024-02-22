using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonCost : MonoBehaviour
{

    [SerializeField] private TMP_Text[] buttonTexts;
    [SerializeField] private BuildManager buildManager;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buttonTexts.Length; i++)
        {
            buttonTexts[i].text = $"BUY: {buildManager.towers[i].cost}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
