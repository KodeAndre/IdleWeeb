using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Controller : MonoBehaviour
{
    public Data data;
    public TMP_Text currencyText;

    public void Start() {
        data = new Data();
    }

    public void Update() {
        currencyText.text = data.currency + " Currency";
    }

    public void AddCurrency() {
        data.currency += 1;
    }

}
