using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    [Tooltip("Ссылка на сам UI")] public GameObject ui;
    [Space(5)]
    [Tooltip("Кнопка апгрейда")] public Button upgradeButton;
    [Tooltip("Цена апгрейда")] public Text upgradeCost;
    [Space(5)]
    [Tooltip("Цена продажи")] public Text sellAmount;

    private Node target;  //Выбранный узел

    /// <summary>
    /// Взаимодействие с выбранным узлом
    /// </summary>
    /// <param name="_target">Выбранный узел</param>
    public void SetTarget (Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.isMaxUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();

        ui.SetActive(true);
    }

    /// <summary>
    /// Спрятать UI
    /// </summary>
    public void Hide()
    {
        ui.SetActive(false);
    }

    /// <summary>
    /// Нажатие на кнопку апгрейда
    /// </summary>
    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.Instance.DeselectNode();
    }

    /// <summary>
    /// Нажатие на кнопку продажи
    /// </summary>
    public void Sell()
    {
        target.SellTurret();
        BuildManager.Instance.DeselectNode();
    }
}
