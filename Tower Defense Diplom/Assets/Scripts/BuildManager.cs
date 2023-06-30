using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }

        Instance = this;
    }

    public GameObject standardTurretPrefab;
    public GameObject missileTurretPrefab;

    public GameObject buildEffect;

    private TurretBlueprint turretToBuild;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void BuildTurretOn (Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;

        GameObject effect = Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        GameObject turret = Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
}
