using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target; // Création de la variable target 
    private int wavepointIndex = 0;
    private Enemy enemy;


    // L'ennemie commence au waypoint 0 de la liste 
    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.points[wavepointIndex];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position; //Direction du vecteur 
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World); // Translation de l'objet "Enemy"

        //Mis en place d'une conditions de secours en cas de bug qui assure un bon fonctionnement du code. Ici, nous allons mettre une durée max où l'ennemie est sur un waypoint

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
        enemy.speed = enemy.startSpeed; //Permet de reset le slow des ennemis quand ils sont hors de portée de la tourelle
    }

    void GetNextWaypoint()
    {
        // Boucle pour détruire l'ennemie une fois arrivé à la fin
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        wavepointIndex++; // Augmenetation de 1 de l'index 
        target = Waypoints.points[wavepointIndex]; //la variable target prend la postion du waypoint suivant
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        Destroy(gameObject);
    }
}
