using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] points; //Création de la liste des positions des waypoints 

    void Awake()
    {
        points = new Transform[transform.childCount]; // On va venir compter le nombre d'enfant(ici les waypoints) qui l'on va sauvegarder dans la liste "points"
        for (int i = 0; i< points.Length; i++)
        {
            points[i] = transform.GetChild(i); // On ajoute chaque enfant i dans la liste en ajoutant 1 par 1 jusqu'a la fin
        }
    }
}
