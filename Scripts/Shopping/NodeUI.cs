using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellAmount;

    private TowerBase target;

    public void SetTarget(TowerBase _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        if (target.isUpgradedTwo == false && target.isUpgradedThree == false)
        {
            upgradeCost.text = "$" + target.turretBluePrint.upgradeCostTwo;
            upgradeButton.interactable = true;
        }
        if (target.isUpgradedTwo == true && target.isUpgradedThree == false)
        {
            upgradeCost.text = "$" + target.turretBluePrint.upgradeCostThree;
            upgradeButton.interactable = true;
        }
        if(target.isUpgradedTwo == true && target.isUpgradedThree == true)
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "$" + target.turretBluePrint.GetSellAmount();

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        if (target.isUpgradedTwo == false && target.isUpgradedThree == false)
        {
            target.UpdateTurretTwo();
            BuildManger.instance.DeselectNode();
        }
        else if (target.isUpgradedTwo == true && target.isUpgradedThree == false)
        {
            target.UpdateTurretThree();
            BuildManger.instance.DeselectNode();
        }
        
    }

    public void Sell()
    {
        target.SellTurret();

        BuildManger.instance.DeselectNode();

        target.isUpgradedTwo = false;
        target.isUpgradedThree = false;
    }
}
