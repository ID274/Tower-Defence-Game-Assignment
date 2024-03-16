using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlotScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color menuColor;

    private GameObject tower;
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
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        sr.color = startColor;
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
            Tower towerToBuild = BuildManager.main.GetSelectedTower();
            if (towerToBuild.cost > 0 && towerToBuild.cost > LevelManager.main.currency)
            {
                return;
            }
            if (towerToBuild.cost > 0)
            {
                LevelManager.main.SpendCurrency(towerToBuild.cost);
                tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
            }
            BuildManager.main.DeselectTower();
            towerToBuild = BuildManager.main.GetSelectedTower();
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
