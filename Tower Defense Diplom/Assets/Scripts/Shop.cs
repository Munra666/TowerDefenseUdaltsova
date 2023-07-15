using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileTurret;
    public TurretBlueprint lazerTurret;

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.Instance;
    }

    public void SelectStandardTurret()
    {
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.selectTurret);
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissileTurret()
    {
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.selectTurret);
        buildManager.SelectTurretToBuild(missileTurret);
    }

    public void SelectLazerTurret()
    {
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.selectTurret);
        buildManager.SelectTurretToBuild(lazerTurret);
    }
}
