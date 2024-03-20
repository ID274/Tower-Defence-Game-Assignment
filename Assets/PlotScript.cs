using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlotScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color menuColor;

    [SerializeField] private GameObject tower;
    private Color startColor;


    private void Start()
    {
        startColor = sr.color;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (tower == null)
        {
            sr.color = hoverColor;
        }
        else
        {
            sr.color = menuColor;
            if (tower.TryGetComponent(out BallistaScript ballistaTowerValues))
            {
                if (ballistaTowerValues.rangeIndicator != null)
                {
                    ballistaTowerValues.rangeIndicator.gameObject.SetActive(true);
                }
            }
            else if (tower.TryGetComponent(out SpearMachineScript spearTowerValues))
            {
                spearTowerValues.rangeIndicator.gameObject.SetActive(true);
            }
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        sr.color = startColor;
        if (tower != null)
        {
            if (tower.TryGetComponent(out BallistaScript ballistaTowerValues))
            {
                if (ballistaTowerValues.rangeIndicator != null)
                {
                    ballistaTowerValues.rangeIndicator.gameObject.SetActive(false);
                }
            }
            else if (tower.TryGetComponent(out SpearMachineScript spearTowerValues))
            {
                spearTowerValues.rangeIndicator.gameObject.SetActive(false);
            }
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Debug.Log(pointerEventData);
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (sr.color != hoverColor && sr.color != menuColor)
        {
            Debug.Log(pointerEventData.pressPosition);
            Debug.Log(pointerEventData.position);
            return;
        }
        if (tower != null)
        {
            OpenTowerMenu();
        }
        else
        {
            Tower towerToBuild = BuildManager.Instance.GetSelectedTower();
            if (towerToBuild.cost > 0 && towerToBuild.cost > LevelManager.Instance.currency)
            {
                return;
            }
            if (towerToBuild.cost > 0)
            {
                LevelManager.Instance.SpendCurrency(towerToBuild.cost);
                tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
            }
            if (!ControlsScript.Instance.placeMoreTowersHeld)
            {
                BuildManager.Instance.DeselectTower();
                towerToBuild = BuildManager.Instance.GetSelectedTower();
            }
            
        }



        

    }

    private void OpenTowerMenu()
    {
        if (!UpgradeMenuScript.Instance.upgradeMenu.activeSelf)
        {
            UpgradeMenuScript.Instance.selectedTower = tower;
            UpgradeMenuScript.Instance.upgradeMenu.SetActive(true);
        }
        else
        {
            if (UpgradeMenuScript.Instance.selectedTower.TryGetComponent(out BallistaScript ballistaTowerValues) && !ballistaTowerValues.iceMachine)
            {
                ballistaTowerValues.rangeIndicator.gameObject.SetActive(false);
            }
            else if (UpgradeMenuScript.Instance.selectedTower.TryGetComponent(out SpearMachineScript spearTowerValues))
            {
                spearTowerValues.rangeIndicator.gameObject.SetActive(false);
            }
            UpgradeMenuScript.Instance.selectedTower = tower;
        }
    }
}
