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

    public List<Upgrades> productionUpgrades;
    public Upgrades productionUpgradePrefab;

    public ScrollRect clickUpgradesScroll;
    public Transform clickUpgradesPanel;

    public ScrollRect productionUpgradesScroll;
    public Transform productionUpgradesPanel;

    public string[] clickUpgradesNames;
    public string[] productionUpgradesNames;

    public BigDouble[] clickUpgradeBaseCost;
    public BigDouble[] clickUpgradeCostMultiplier;
    public BigDouble[] clickUpgradesBasePower;
    public BigDouble[] clickUpgradesUnlock;

    public BigDouble[] productionUpgradeBaseCost;
    public BigDouble[] productionUpgradeCostMultiplier;
    public BigDouble[] productionUpgradesBasePower;
    public BigDouble[] productionUpgradesUnlock;

    public void StartUpgradeManager() {
        Methods.UpgradeCheck(Controller.instance.data.clickUpgradeLevel, 4);

        clickUpgradesNames = new []{"Click Power +1", "Click Power +5", "Click Power +10", "Click Power +25"};
        productionUpgradesNames = new []{"Currency +1/s", "Currency +2/s", "Currency +10/s", "Currency +100/s"};

        clickUpgradeBaseCost = new BigDouble[]{10, 50, 100, 1000};
        clickUpgradeCostMultiplier = new BigDouble[]{1.25, 1.35, 1.55, 2};
        clickUpgradesBasePower = new BigDouble[]{1, 5, 10, 25};
        clickUpgradesUnlock = new BigDouble[]{0, 25, 50, 500};

        productionUpgradeBaseCost = new BigDouble[]{25, 100, 1000, 10000};
        productionUpgradeCostMultiplier = new BigDouble[]{1.5, 1.75, 2, 3};
        productionUpgradesBasePower = new BigDouble[]{1, 2, 10, 100};
        productionUpgradesUnlock = new BigDouble[]{0, 50, 500, 5000};

        for (int i = 0; i < Controller.instance.data.clickUpgradeLevel.Count; i++) {
            Upgrades upgrade = Instantiate(clickUpgradePrefab, clickUpgradesPanel);
            upgrade.UpgradeID = i;
            upgrade.gameObject.SetActive(false);
            clickUpgrades.Add(upgrade);
        }

        for (int i = 0; i < Controller.instance.data.productionUpgradeLevel.Count; i++) {
            Upgrades upgrade = Instantiate(productionUpgradePrefab, productionUpgradesPanel);
            upgrade.UpgradeID = i;
            upgrade.gameObject.SetActive(false);
            productionUpgrades.Add(upgrade);
        }

        clickUpgradesScroll.normalizedPosition = new Vector2(0, 0);
        productionUpgradesScroll.normalizedPosition = new Vector2(0, 0);

        UpdateUpgradeUI("click");
        UpdateUpgradeUI("production");

    }

    public void Update() {
        for (int i = 0; i < clickUpgrades.Count; i++) {
            if (!clickUpgrades[i].gameObject.activeSelf) clickUpgrades[i].gameObject.SetActive(Controller.instance.data.currency >= clickUpgradesUnlock[i]);
        }
        for (int i = 0; i < productionUpgrades.Count; i++) {
            if (!productionUpgrades[i].gameObject.activeSelf) productionUpgrades[i].gameObject.SetActive(Controller.instance.data.currency >= productionUpgradesUnlock[i]);
        }
    }

    public void UpdateUpgradeUI(string type, int UpgradeID = -1) {
        var data = Controller.instance.data;

        void UpdateUI(List<Upgrades> upgrades, List<BigDouble> upgradeLevel, string[] upgradeNames, int ID) {
                upgrades[ID].LevelText.text = upgradeLevel[ID].ToString();
                upgrades[ID].CostText.text = $"Cost: {UpgradeCost(type, ID):F2} Currency";
                upgrades[ID].NameText.text = upgradeNames[ID];
        }

        switch (type)
        {
            case "click":
                if (UpgradeID == -1) {
                    for (int i = 0; i < clickUpgrades.Count; i++) UpdateUI(clickUpgrades, data.clickUpgradeLevel, clickUpgradesNames, i);
                }
                else UpdateUI(clickUpgrades, data.clickUpgradeLevel, clickUpgradesNames, UpgradeID);
                break;
            case "production":
                if (UpgradeID == -1) {
                    for (int i = 0; i < productionUpgrades.Count; i++) UpdateUI(productionUpgrades, data.productionUpgradeLevel, productionUpgradesNames, i);
                }
                else UpdateUI(productionUpgrades, data.productionUpgradeLevel, productionUpgradesNames, UpgradeID);
                break;
        }

    }

    public BigDouble UpgradeCost(string type, int UpgradeID) {
        var data = Controller.instance.data;
        switch (type) {
            case "click":
                return clickUpgradeBaseCost[UpgradeID] * BigDouble.Pow(clickUpgradeCostMultiplier[UpgradeID], data.clickUpgradeLevel[UpgradeID]);
            case "production":
                return productionUpgradeBaseCost[UpgradeID] * BigDouble.Pow(productionUpgradeCostMultiplier[UpgradeID], data.productionUpgradeLevel[UpgradeID]);
        }
        return 0;
    }

    public void BuyUpgrade(string type, int UpgradeID) {
        var data = Controller.instance.data;
        UpdateUpgradeUI(type, UpgradeID);

        switch (type) {
            case "click": Buy(data.clickUpgradeLevel); break;
            case "production": Buy(data.productionUpgradeLevel); break;
        }
        void Buy(List<BigDouble> upgradeLevels) {
            if (data.currency >= UpgradeCost(type, UpgradeID)) {
                data.currency -= UpgradeCost(type, UpgradeID);
                upgradeLevels[UpgradeID] += 1;
            }
        }
    }

}
