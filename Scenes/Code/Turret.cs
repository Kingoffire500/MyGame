using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target; // Enregistrer notre cible

    public float range = 15f; // Portée du guidage 
    public float fireRate = 1f; // permet de régler la fréquence de tir
    private float fireCountdown = 0f;

    public string enemyTag = "Enemy";
    public float turnSpeed = 10f; // Vitesse de rotation de la tourelle
    public Transform partToRotate;

    public GameObject bulletPrefab;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); // On veut mettre à jour cette fonctione 2 fois par seconde
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); // On va enregistrer les ennemis présent dans la scene qui possède la Tag "Enemy"
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies) // Permet d'excuter la boucle for pour chaque ennemis de la scene 
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);// Permet de connaitre la distance entre la tourelle et l'ennemi 
            if (distanceToEnemy < shortestDistance) // On veut voir si la distance entre l'ennemi est la plus petite distance que les autres 
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
                  
            }

        }

        if (nearestEnemy != null && shortestDistance <= range ) // On pose une conditions : si jamais on trouve un ennemi et si celui ci est la portée de la tourelle
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        //Target lock on 
        Vector3 dir = target.position - transform.position; // On crée un vecteur pour connaitre la direction dans laquelle doit regarder la tourelle
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles; // On passe en angle d'Euler pour éviter de d'utiliser les quaternions
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f); // On autorise seulement une rotation selon y

        if (fireCountdown <= 0f)  // Cela sert à donner un vitesse de tir 
        {
            Shoot();
            fireCountdown = 1f / fireRate; 
        }
        fireCountdown -= Time.deltaTime; // On baisse d'un cran toute les secondes
    }
    // Permet d'afficher dans l'editeur la porté de la tourelle avec des spheres vides rouges 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // On fait spawn la bullet au niveau de notre game object appelé firePoint
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
            
    }
}
