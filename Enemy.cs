using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]
public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f; // Permet de g�rer la vitesse des ennemis (reset de la vitesse apr�s ralentissement du laser)
    [HideInInspector]
    public float speed ; // Permet de g�rer la vitesse des ennemis
    private Transform target; // Cr�ation de la variable target 

    private int wavepointIndex = 0;
    private Enemy enemy;

    public float health = 100; // point de vie des ennemis
    public int value = 50;

    // fonction permettant de r�duire la vie des ennemis
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0) // condition qui d�truit les ennemis si ils ont 0 point de vie
        {
            Die();
        }
    }
    // fonction permettant de d�truire les ennemis
    void Die()
    {
        PlayerStats.Money += value; // quand un ennemis est d�truit on gagne de l'argent 
        Destroy(gameObject);
        WaveSpawner.EnemiesAlive--;

    }
    // fonction permettant de ralentir les ennemis 
    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct); // on d�finit un nombre entre 0 et 1 qui correspond au pourcentage de ralentissement
    }
    


    // L'ennemie commence au waypoint 0 de la liste 
    void Start()
    {
        target = Waypoints.points[wavepointIndex];
    }
    void Update()
    {
        Vector3 dir = target.position - transform.position; //Direction du vecteur 
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World); // Translation de l'objet "Enemy"

        //Mis en place d'une conditions de secours en cas de bug qui assure un bon fonctionnement du code. Ici, nous allons mettre une dur�e max o� l'ennemie est sur un waypoint

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
        speed = startSpeed;
    }
    void GetNextWaypoint()
    {
        // Boucle pour d�truire l'ennemie une fois arriv� � la fin
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
        PlayerStats.Lives--; // r�duit le nombre de vies du joueur quand un ennemis arrive au bout du chemin
        Destroy(gameObject);
        WaveSpawner.EnemiesAlive--;
    }

}
