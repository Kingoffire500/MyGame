using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance; // On va creer un buildmanager dans la fonction build manager pour l'utiliser plus facilement et partout dans les différent script qu'on utilisera par la suite 
    // Utilisation d'un singleton pattern pour ne pas utiliser trop de calcul car sinon chaque node possède une réference différente et il faut rentrer ces réferences par nous même dans le code 
    void Awake()
    {
        if (instance != null) // Attention, il faut seulement un seul buildManager car sinon instance va être égale à 2 BuildManager différent ce qui est impossible d'où cette condition
        {
            Debug.LogError("already have a buildManager open ");
            return;
        }
        instance = this; // permet avant la fonction start de dire que instance devient le buildManager 
         
    }
    public GameObject standardTurretPrefab;
    public GameObject explosiveTurretPrefab;
    public GameObject laserTurretPrefab;

    private TurretBlueprint turretToBuild; 
    private Node selectedNode;

    public NodeUI nodeUI;

    public bool CanBuild { get { return turretToBuild != null; } } // permet de mettre une condition directement sur la variable CanBuild (si turretToBuild est different de null alors CanBuild = True)

    public bool AsMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }//Condition pour voir si on a assez d'argent pour faire la tourelle 

    public void SelectNode (Node node) //Selectionne le node voulu
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

    public void SelectTurretToBuild(TurretBlueprint turret) //Selectionne la tourelle voulu d'après le blueprint
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }



    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
    

}
