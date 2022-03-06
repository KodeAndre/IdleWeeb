using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BreakInfinity;

public class Controller : MonoBehaviour
{
    public static Controller instance;
    private void Awake() => instance = this;

    public Data data;
    [SerializeField] private TMP_Text currencyText;
    [SerializeField] private TMP_Text currencyPerSecondText;
    [SerializeField] private TMP_Text currencyClickPowerText;

    public BigDouble ClickPower() {
        BigDouble total = 1;
        for (int i = 0; i < data.clickUpgradeLevel.Count; i++) {
            total += UpgradesManager.instance.clickUpgradesBasePower[i] * data.clickUpgradeLevel[i];
        }
        return total;
    }

    public BigDouble CurrencyPerSecond() {
        BigDouble total = 0;
        for (int i = 0; i < data.productionUpgradeLevel.Count; i++) {
            total += UpgradesManager.instance.productionUpgradesBasePower[i] * data.productionUpgradeLevel[i];
        }
        return total;
    }

    private const string dataFileName = "PlayerData";
    public void Start() {
        data = SaveSystem.SaveExists(dataFileName) ? SaveSystem.LoadData<Data>(dataFileName) : new Data();
        UpgradesManager.instance.StartUpgradeManager();
    }

    public float SaveTime;

    public void Update() {
        currencyText.text = $"{data.currency:F2} Currency";
        currencyPerSecondText.text = $"{CurrencyPerSecond():F2} /s";
        currencyClickPowerText.text = $"+{ClickPower()} Currency";

        data.currency = data.currency + CurrencyPerSecond() * Time.deltaTime;

        SaveTime += Time.deltaTime * (1 / Time.timeScale);
        if (SaveTime >= 15) {
            SaveSystem.SaveData(data, dataFileName);
            SaveTime = 0;
        }
    }

    public void AddCurrency() {
        data.currency += ClickPower();
    }

}
