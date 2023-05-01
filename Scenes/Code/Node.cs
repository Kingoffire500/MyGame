using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour
{
    private Renderer rend;
    public GameObject turret;
    public Color Couleur;
    public Vector3 PositionOffSet;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void OnMouseDown() // S'active quand on click sur le collider
    {
        if (!BuildManager.instance.CanBuild ) //permet de poser une tourelle seulement si on en à selectionner une dans le shop 
        {
            return;
        }
        if (turret != null) // condition pour éviter de placer plusieurs tourelles sur la même dalle 
        {
            Debug.Log(" ne peux pas construire");
            return;
        }
        BuildManager.instance.BuildTurretON(this);
    }
    // The mesh goes red when the mouse is over it...
    void OnMouseEnter()
    {
        if (!BuildManager.instance.CanBuild)
        {
            return;
        }
        rend.material.color = Couleur;
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