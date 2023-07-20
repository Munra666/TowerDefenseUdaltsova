using UnityEngine;

public class Shop : MonoBehaviour
{
    [Tooltip("План стандартной туррели")] public TurretBlueprint standardTurret;
    [Tooltip("План Ракетной туррели")] public TurretBlueprint missileTurret;
    [Tooltip("План лазерной туррели")] public TurretBlueprint lazerTurret;
    [Space(5)]
    [Tooltip("Кнопки с выбором туррелей")] public GameObject[] buttonsChoiceTurret;

    private BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.Instance;
    }

    private void Update()
    {
        if(buildManager.CanBuild == false)
        {
            DeselectTurrets();
        }
    }

    /// <summary>
    /// Выбор Стандартной туррели для строительства
    /// </summary>
    public void SelectStandardTurret()
    {
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.selectTurret);
        buildManager.SelectTurretToBuild(standardTurret);

        DeselectTurrets();
        RectTransform rt = buttonsChoiceTurret[0].GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(120, 120);
    }

    /// <summary>
    /// Выбор Ракетной туррели для строительства
    /// </summary>
    public void SelectMissileTurret()
    {
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.selectTurret);
        buildManager.SelectTurretToBuild(missileTurret);

        DeselectTurrets();
        RectTransform rt = buttonsChoiceTurret[1].GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(120, 120);
    }

    /// <summary>
    /// Выбор Лазерной туррели для строительства
    /// </summary>
    public void SelectLazerTurret()
    {
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.selectTurret);
        buildManager.SelectTurretToBuild(lazerTurret);

        DeselectTurrets();
        RectTransform rt = buttonsChoiceTurret[2].GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(120, 120);
    }

    /// <summary>
    ///  Возврат иконок к стандартному размеру если нет выбранной туррели для строительства
    /// </summary>
    public void DeselectTurrets()
    {
        foreach (GameObject button in buttonsChoiceTurret)
        {
            RectTransform rt = button.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(100, 100);
        }
    }
}
