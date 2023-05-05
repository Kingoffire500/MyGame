using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    private Renderer rend;
    public GameObject turret;
    public TurretBlueprint turretBlueprint;
    public bool isUpgraded = false;
    public Color Couleur;
    public Vector3 PositionOffSet;
    public Vector3 position;

    public Vector3 GetBuildPosition()
    {
        return transform.position + position;
    }
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void OnMouseDown() // S'active quand on click sur le collider
    {
        if (EventSystem.current.IsPointerOverGameObject())  //Permet d'eviter de lorsque l'on clique sur le bouton, on pose uen tourelle derrière 
        {
            return;
        }
       
        if (turret != null) // condition pour éviter de placer plusieurs tourelles sur la même dalle 
        {
            BuildManager.instance.SelectNode(this);
            return;
        }
        if (!BuildManager.instance.CanBuild) //permet de poser une tourelle seulement si on en à selectionner une dans le shop 
        {
            return;
        }
        //BuildManager.instance.BuildTurretON(this); //Pose la tourelle 
        BuildTurret(BuildManager.instance.GetTurretToBuild());
    }
    void BuildTurret (TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough");
            return;
        }
        turretBlueprint = blueprint;
        PlayerStats.Money -= blueprint.cost; //On enlève de l'argent 
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, transform.position + PositionOffSet, transform.rotation); //On place la tourelle 
        turret = _turret;

        Debug.Log("Turret money left" + PlayerStats.Money);
    }
    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough");
            return;
        }
        //destroy the old turret
        Destroy(turret);
        
        PlayerStats.Money -= turretBlueprint.upgradeCost; //On enlève de l'argent 
        //Build a new one
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, transform.position + PositionOffSet, transform.rotation); //On place la tourelle 
        turret = _turret;

        isUpgraded = true;

        Debug.Log("Turret upgrade");
    }

    // The mesh goes red when the mouse is over it...
    void OnMouseEnter()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!BuildManager.instance.CanBuild)
        {
            return;
        }
        if (BuildManager.instance.AsMoney)
        {
            rend.material.color = Couleur;
        }
        else
        {
            rend.material.color = Color.red;
        }
    }
        


    // ...and the mesh finally turns white when the mouse moves away.
    void OnMouseExit()
    {
        if (!BuildManager.instance.CanBuild)
        {
            return;
        }
        rend.material.color = Color.white;
    }
}