using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [Tooltip("Цвет выбранного узла")] public Color hoverColor;
    [Tooltip("Цвет запрета строительства")] public Color notEnoughMoneyColor;

    [HideInInspector]
    public GameObject turret;  //Туррель стоящая на узле
    [HideInInspector]
    public TurretBlueprint turretBlueprint;  //План туррели
    [HideInInspector]
    public bool isMaxUpgraded = false;  //Конечная ли стадия апгрейда
    [HideInInspector]
    public int upgradeLevelNumber = 0;  //Уровень прокачки
    [HideInInspector]
    public float healthTurretLoad;  //Сколько жизней загрузить туррели

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.Instance;
    }

    private void Update()
    {
        if(turret != null && turret.GetComponent<Turret>().isDestruction)
        {
            DestructionTurret();
        }
    }

    /// <summary>
    /// Позиция строительства
    /// </summary>
    /// <returns></returns>
    public Vector3 GetBuildPosition()
    {
        return transform.position;
    }


    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if(turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

    /// <summary>
    /// Постройка первой туррели
    /// </summary>
    /// <param name="blueprint">План туррели</param>
    private void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        AudioManager.Instance.OneShotPlay(AudioManager.Instance.apgradeTurret);

        GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        upgradeLevelNumber++;

        turretBlueprint = blueprint;
    }

    /// <summary>
    /// Апгрейд уже стоящей туррели
    /// </summary>
    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            AudioManager.Instance.OneShotPlay(AudioManager.Instance.noMoney);
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;

        Destroy(turret);

        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        AudioManager.Instance.OneShotPlay(AudioManager.Instance.apgradeTurret);

        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefabs[upgradeLevelNumber - 1], 
            GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        upgradeLevelNumber++;

        if (upgradeLevelNumber > turretBlueprint.upgradedPrefabs.Length)
        {
            isMaxUpgraded = true;
        }
    }

    /// <summary>
    /// Продажа туррели
    /// </summary>
    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();

        GameObject effect = Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        AudioManager.Instance.OneShotPlay(AudioManager.Instance.destructionTurret);

        Destroy(turret);
        turretBlueprint = null;

        isMaxUpgraded = false;
        upgradeLevelNumber = 0;
    }

    /// <summary>
    /// Уничтожение туррели если закончились ее жизни
    /// </summary>
    private void DestructionTurret()
    {
        GameObject effect = Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        AudioManager.Instance.OneShotPlay(AudioManager.Instance.destructionTurret);

        Destroy(turret);
        turretBlueprint = null;

        isMaxUpgraded = false;
        upgradeLevelNumber = 0;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            AudioManager.Instance.OneShotPlay(AudioManager.Instance.noMoney);
            rend.material.color = notEnoughMoneyColor;
        }
        
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    /// <summary>
    /// Загрузка данных туррели
    /// </summary>
    public void Load()
    {
        if (upgradeLevelNumber == 1)
        {
            GameObject _turret = Instantiate(turretBlueprint.prefab, GetBuildPosition(), Quaternion.identity);
            turret = _turret;
        }
        else if (upgradeLevelNumber > 1)
        {
            GameObject _turret = Instantiate(turretBlueprint.upgradedPrefabs[upgradeLevelNumber - 2],
            GetBuildPosition(), Quaternion.identity);
            turret = _turret;
        }

        if (upgradeLevelNumber >= turretBlueprint.upgradedPrefabs.Length)
            isMaxUpgraded = true;

        if (turret != null)
        {
            Turret t = turret.GetComponent<Turret>();
            t.health = healthTurretLoad;
            t.healthBar.fillAmount = t.health / t.startHealth;
        }
    }
}
