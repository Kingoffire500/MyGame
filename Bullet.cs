using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 70f; // d�finit la vitesse du projectile
    public float explosionRadius = 0f; // d�finit la distance d'explosion du misile
    public GameObject impactEffect;

    public int damage = 20;
    public void Seek(Transform _target) // Pour aller chercher la target que j'ai fait dans le code Enemy
    {
        target = _target;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        if (dir.magnitude <= distanceThisFrame) // Permet d'eviter de shoot apr�s que l'ennemi soit pass� 
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World); // On fait bouger la bullet 
        transform.LookAt(target);
    }
    void HitTarget()
    {
        if (explosionRadius > 0f)
        {
            Explode(); // fait exploser le missile si il a un rayon d'explosion 
        } else
        {
            Damage(target); // sinon il n'explose pas et fait les d�gats de base 
        }
        
        Destroy(gameObject);
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);
    }

    // fonction permettant de rep�rer s'il y a des ennemis dans la distance d'explosion et si oui applique les d�gats � tous les ennemis pr�sent dedans
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    // fonction qui applique des d�gats aux ennemis
    void Damage(Transform enemy)
    {

        Enemy e =enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(damage);
        }
        
       
    }

    // Dessine un cercle qui affiche la port�e pour l'�diteur
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
