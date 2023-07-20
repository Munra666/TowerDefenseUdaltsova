using UnityEngine.UI;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [Tooltip("Отображение количества денег")] public Text moneyText;

    private void Update()
    {
        moneyText.text = "$" + PlayerStats.Money.ToString();
    }
}
