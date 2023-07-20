using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    [Tooltip("Индекс Плана для его сохранения и загрузки")] public int index;
    [Space(5)]
    [Tooltip("Начальная туррель")] public GameObject prefab;
    [Tooltip("стоимость постройки")] public int cost;
    [Space(5)]
    [Tooltip("Ступени апгрейда по порядку")] public GameObject[] upgradedPrefabs;
    [Tooltip("Стоимость апгрейда")] public int upgradeCost;

    /// <summary>
    /// Нахождение стоимости продажи
    /// </summary>
    /// <returns></returns>
    public int GetSellAmount()
    {
        return cost / 2;
    }
}
