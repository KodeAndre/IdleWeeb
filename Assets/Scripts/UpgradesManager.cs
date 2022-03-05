using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreakInfinity;

public class UpgradesManager : MonoBehaviour
{
    public Controller controller;

    public Upgrades clickUpgrade;
    public string clickUpgradeName;
    public BigDouble clickUpgradeBaseCost;
    public BigDouble clickUpgradeCostMultiplier;

    public void StartUpgradeManager() {
        clickUpgradeName = "Currency Per Click";
        clickUpgradeBaseCost = 10;
        clickUpgradeCostMultiplier = 1.5;
        UpdateClickUpgradeUI();
    }

    public void Update() {

    }

    public void UpdateClickUpgradeUI() {
        clickUpgrade.LevelText.text = controller.data.clickUpgradeLevel.ToString();
        clickUpgrade.CostText.text = "Cost: " + Cost().ToString("F2") + " Currency";
        clickUpgrade.NameText.text = "+1 " + clickUpgradeName;
    }

    public BigDouble Cost() => clickUpgradeBaseCost * BigDouble.Pow(clickUpgradeCostMultiplier, controller.data.clickUpgradeLevel);

    public void BuyUpgrade() {
        if (controller.data.currency >= Cost()) {
            controller.data.currency -= Cost();
            controller.data.clickUpgradeLevel += 1;
        }
        UpdateClickUpgradeUI();
    }

}
