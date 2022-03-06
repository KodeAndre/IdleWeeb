using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreakInfinity;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager instance;
    private void Awake() => instance = this;
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
        var data = Controller.instance.data;
        clickUpgrade.LevelText.text = data.clickUpgradeLevel.ToString();
        clickUpgrade.CostText.text = "Cost: " + Cost().ToString("F2") + " Currency";
        clickUpgrade.NameText.text = "+1 " + clickUpgradeName;
    }

    public BigDouble Cost() => clickUpgradeBaseCost * BigDouble.Pow(clickUpgradeCostMultiplier, Controller.instance.data.clickUpgradeLevel);

    public void BuyUpgrade() {
        if (Controller.instance.data.currency >= Cost()) {
            Controller.instance.data.currency -= Cost();
            Controller.instance.data.clickUpgradeLevel += 1;
        }
        UpdateClickUpgradeUI();
    }

}
