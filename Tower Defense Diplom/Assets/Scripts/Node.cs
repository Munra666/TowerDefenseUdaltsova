using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isMaxUpgraded = false;
    private int upgradeLevelnumber = 0;

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

        turretBlueprint = blueprint;
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;

        Destroy(turret);

        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        AudioManager.Instance.OneShotPlay(AudioManager.Instance.apgradeTurret);

        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefabs[upgradeLevelnumber], 
            GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        upgradeLevelnumber++;

        if (upgradeLevelnumber >= turretBlueprint.upgradedPrefabs.Length)
        {
            isMaxUpgraded = true;
        }
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();

        GameObject effect = Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        AudioManager.Instance.OneShotPlay(AudioManager.Instance.destructionTurret);

        Destroy(turret);
        turretBlueprint = null;

        isMaxUpgraded = false;
        upgradeLevelnumber = 0;
    }

    private void DestructionTurret()
    {
        GameObject effect = Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        AudioManager.Instance.OneShotPlay(AudioManager.Instance.destructionTurret);

        Destroy(turret);
        turretBlueprint = null;

        isMaxUpgraded = false;
        upgradeLevelnumber = 0;
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
}
