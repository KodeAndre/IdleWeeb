using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BreakInfinity;

public class Controller : MonoBehaviour
{
    public UpgradesManager upgradesManager;
    public Data data;
    [SerializeField] private TMP_Text currencyText;
    [SerializeField] private TMP_Text currencyClickPowerText;

    public BigDouble ClickPower() => 1 + data.clickUpgradeLevel;

    public void Start() {
        data = new Data();
        upgradesManager.StartUpgradeManager();
    }

    public void Update() {
        currencyText.text = data.currency.ToString("F2") + " Currency";
        currencyClickPowerText.text = "+" + ClickPower() + " Currency";
    }

    public void AddCurrency() {
        data.currency += ClickPower();
    }

}
