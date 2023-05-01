using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint explosiveTurret;
    public TurretBlueprint laserTurret;

    BuildManager A;
    void Start()
    {
        A = BuildManager.instance;
    }
   public void SelectStandardTurret()
    {
        Debug.Log("standard turret acheté");
        A.SelectTurretToBuild(standardTurret);

    }
    public void SelectExplosiveTurret()
    {
        Debug.Log("standard turret acheté");
        A.SelectTurretToBuild(explosiveTurret);

    }
    public void SelectLaserTurret()
    {
        Debug.Log("standard turret acheté");
        A.SelectTurretToBuild(laserTurret);

    }
}
