using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] public Tower[] towers;
    [SerializeField] public GameObject[] towersSelectedBackground;

    private int selectedTower = 5;

    private void Awake()
    {
        main = this;
        foreach (var GameObject in towersSelectedBackground)
        {
            GameObject.SetActive(false);
        }
        //towersSelectedBackground[0].SetActive(true);
    }

    public Tower GetSelectedTower()
    {
        return towers[selectedTower];
    }

    public void SetSelectedTower(int _selectedTower)
    {
        if (!LevelManager.main.gameOver)
        {
            if (selectedTower == _selectedTower)
            {
                selectedTower = 5;
                foreach (var GameObject in towersSelectedBackground)
                {
                    GameObject.SetActive(false);
                }
                towersSelectedBackground[selectedTower].SetActive(true);
            }
            else
            {
                selectedTower = _selectedTower;
                foreach (var GameObject in towersSelectedBackground)
                {
                    GameObject.SetActive(false);
                }
                towersSelectedBackground[selectedTower].SetActive(true);
            }
        }
    }

}
