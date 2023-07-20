using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    [Tooltip("Эффект строительства туррели")] public GameObject buildEffect;
    [Tooltip("Эффект продажи туррели")] public GameObject sellEffect;
    [Space(5)]
    [Tooltip("UI узла с настройками туррели")] public NodeUI nodeUI;

    private TurretBlueprint turretToBuild; //План туррели, где описаны варианты апгрейла и стоимость
    private Node selectedNode; //Выбранный узел для строительства

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }

        Instance = this;
    }

    /// <summary>
    /// Можно ли троить на выбранном узле
    /// </summary>
    public bool CanBuild { get { return turretToBuild != null; } }

    /// <summary>
    /// Хватает ли денег на строительство
    /// </summary>
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    /// <summary>
    /// Выбранный узел
    /// </summary>
    /// <param name="node">Узел</param>
    public void SelectNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }
        
        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    /// <summary>
    /// Отмена выбора узла
    /// </summary>
    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    /// <summary>
    /// Выбор туррели для строительства
    /// </summary>
    /// <param name="turret"></param>
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    /// <summary>
    /// Возвращает план туррели которую надо построить
    /// </summary>
    /// <returns></returns>
    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
