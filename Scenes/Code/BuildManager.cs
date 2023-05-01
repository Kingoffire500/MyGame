using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance; // On va creer un buildmanager dans la fonction build manager pour l'utiliser plus facilement et partout dans les diff�rent script qu'on utilisera par la suite 
    // Utilisation d'un singleton pattern pour ne pas utiliser trop de calcul car sinon chaque node poss�de une r�ference diff�rente et il faut rentrer ces r�ferences par nous m�me dans le code 
    void Awake()
    {
        if (instance != null) // Attention, il faut seulement un seul buildManager car sinon instance va �tre �gale � 2 BuildManager diff�rent ce qui est impossible d'o� cette condition
        {
            Debug.LogError("already have a buildManager open ");
            return;
        }
        instance = this; // permet avant la fonction start de dire que instance devient le buildManager 
         
    }
    public GameObject standardTurretPrefab;
    public GameObject explosiveTurretPrefab;
    public GameObject laserTurretPrefab;

    private TurretBlueprint turretToBuild; // permet de savoir qu'elle tourelle on veut construire D

    public bool CanBuild { get { return turretToBuild != null; } } // permet de mettre une condition directement sur la variable CanBuild (si turretToBuild est different de null alors CanBuild = True)

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
    
    public void BuildTurretON(Node node) //Construction de la tourelle 
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough");
            return;
        }
        PlayerStats.Money -= turretToBuild.cost;
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.transform.position + node.PositionOffSet, node.transform.rotation);
        node.turret = turret;

        Debug.Log("Turret money left" + PlayerStats.Money);
    }
    
}
