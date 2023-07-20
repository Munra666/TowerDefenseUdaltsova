using UnityEngine;

public class LoadSaveNodes : MonoBehaviour
{
    public static LoadSaveNodes Instance;

    [Tooltip("Все узлы на карте")] public Node[] nodes;

    [Tooltip("Магазин с планами туррелей")] public Shop shop;

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        Load();
    }

    /// <summary>
    /// Сохранение жизней туррелей, срабатывает в моменте нанесения урона по ним
    /// </summary>
    public void SaveHealthTurrets()
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            if(nodes[i].turret != null)
                PlayerPrefs.SetFloat("turretHealth" + i.ToString(), nodes[i].turret.GetComponent<Turret>().health);
            else
                PlayerPrefs.SetFloat("turretHealth" + i.ToString(), 0);
        }
    }

    /// <summary>
    /// Сохранение данных узлов
    /// </summary>
    public void Save()
    {
        for (int i = 0; i < nodes.Length; i++)
        {
               PlayerPrefs.SetInt("node_upgradeLevelNumber" + i.ToString(), nodes[i].upgradeLevelNumber);

            if (nodes[i].turretBlueprint != null)
                PlayerPrefs.SetInt("node_turretBlueprint" + i.ToString(), nodes[i].turretBlueprint.index);
            else
                PlayerPrefs.SetInt("node_turretBlueprint" + i.ToString(), 0);
        }
    }

    /// <summary>
    /// Загрузка данных узлов
    /// </summary>
    private void Load()
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            if (PlayerPrefs.HasKey("node_upgradeLevelNumber" + i.ToString()))
            {
                nodes[i].upgradeLevelNumber = PlayerPrefs.GetInt("node_upgradeLevelNumber" + i.ToString());

                if (PlayerPrefs.GetInt("node_turretBlueprint" + i.ToString()) == 1)
                    nodes[i].turretBlueprint = shop.standardTurret;
                else if (PlayerPrefs.GetInt("node_turretBlueprint" + i.ToString()) == 2)
                    nodes[i].turretBlueprint = shop.missileTurret;
                else if (PlayerPrefs.GetInt("node_turretBlueprint" + i.ToString()) == 3)
                    nodes[i].turretBlueprint = shop.lazerTurret;

                nodes[i].healthTurretLoad = PlayerPrefs.GetFloat("turretHealth" + i.ToString());

                nodes[i].Load();
            }
        }
    }

    private void OnDisable()
    {
        Save();
    }
}