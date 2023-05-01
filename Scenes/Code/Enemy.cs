using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f; // Permet de g�rer la vitesse des ennemis
    private Transform target; // Cr�ation de la variable target 
    private int wavepointIndex = 0;

    // L'ennemie commence au waypoint 0 de la liste 
    void Start ()
    {
        target = Waypoints.points[wavepointIndex];
    }
    void Update()
    {
        Vector3 dir = target.position - transform.position; //Direction du vecteur 
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World); // Translation de l'objet "Enemy"

        //Mis en place d'une conditions de secours en cas de bug qui assure un bon fonctionnement du code. Ici, nous allons mettre une dur�e max o� l'ennemie est sur un waypoint

        if(Vector3.Distance(transform.position, target.position) <=0.2f)
        {
            GetNextWaypoint();
        }

    }
    void GetNextWaypoint()
    {
        // Boucle pour d�truire l'ennemie une fois arriv� � la fin
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        wavepointIndex++; // Augmenetation de 1 de l'index 
        target = Waypoints.points[wavepointIndex]; //la variable target prend la postion du waypoint suivant
    }
}
