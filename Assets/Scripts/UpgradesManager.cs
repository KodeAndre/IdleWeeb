using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BreakInfinity;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager instance;
    private void Awake() => instance = this;

    public List<Upgrades> clickUpgrades;
    public Upgrades clickUpgradePrefab;

    public ScrollRect clickUpgradesScroll;
    public Transform clickUpgradesPanel;

    public string[] clickUpgradesNames;
    public string clickUpgradeName;
    public BigDouble[] clickUpgradeBaseCost;
    public BigDouble[] clickUpgradeCostMultiplier;
    public BigDouble[] clickUpgradesBasePower;

    public void StartUpgradeManager() {
        Methods.UpgradeCheck(ref Controller.instance.data.clickUpgradeLevel, 4);

        clickUpgradesNames = new []{"Click Power +1", "Click Power +5", "Click Power +10", "Click Power +25"};
        clickUpgradeBaseCost = new BigDouble[]{10, 50, 100, 1000};
        clickUpgradeCostMultiplier = new BigDouble[]{1.25, 1.35, 1.55, 2};
        clickUpgradesBasePower = new BigDouble[]{1, 5, 10, 25};

        for (int i = 0; i < Controller.instance.data.clickUpgradeLevel.Count; i++) {
            Upgrades upgrade = Instantiate(clickUpgradePrefab, clickUpgradesPanel);
            upgrade.UpgradeID = i;
            clickUpgrades.Add(upgrade);
        }
        clickUpgradesScroll.normalizedPosition = new Vector2(0, 0);

        UpdateClickUpgradeUI();

    }

    public void Update() {

    }

    public void UpdateClickUpgradeUI(int UpgradeID = -1) {
        var data = Controller.instance.data;

        if (UpgradeID == -1) {
            for (var i = 0; i < clickUpgrades.Count; i++) UpdateUI(i);
        }
        else UpdateUI(UpgradeID);

        void UpdateUI(int ID) {
                clickUpgrades[ID].LevelText.text = data.clickUpgradeLevel[ID].ToString();
                clickUpgrades[ID].CostText.text = $"Cost: {ClickUpgradeCost(ID):F2} Currency";
                clickUpgrades[ID].NameText.text = clickUpgradesNames[ID];
        }
    }

    public BigDouble ClickUpgradeCost(int UpgradeID) => clickUpgradeBaseCost[UpgradeID] * BigDouble.Pow(clickUpgradeCostMultiplier[UpgradeID], Controller.instance.data.clickUpgradeLevel[UpgradeID]);

    public void BuyUpgrade(int UpgradeID) {
        if (Controller.instance.data.currency >= ClickUpgradeCost(UpgradeID)) {
            Controller.instance.data.currency -= ClickUpgradeCost(UpgradeID);
            Controller.instance.data.clickUpgradeLevel[UpgradeID] += 1;
        }
        UpdateClickUpgradeUI(UpgradeID);
    }

}
