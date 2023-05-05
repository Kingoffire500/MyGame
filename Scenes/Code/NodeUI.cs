using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{

    public GameObject UI;
    private Node target;
    public Text upgradeCost;
    public Button upgradeButton;

    public void SetTarget(Node _target) // Permet de faire apparaitre l'UI de selection
    {
        target = _target;
        transform.position = target.GetBuildPosition();
       
        if(!target.isUpgraded)
        {
            upgradeCost.text = target.turretBlueprint.upgradeCost + "€";
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "done";
            upgradeButton.interactable = false;
        }
        UI.SetActive(true);
    }
            
    public void Hide() // Enleve le canvas de l'UI
    {
        UI.SetActive(false);

    }
    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

       
}
