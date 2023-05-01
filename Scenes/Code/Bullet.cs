using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 70f;
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
        if (dir.magnitude <= distanceThisFrame) // Permet d'eviter de shoot après que l'ennemi soit passé 
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World); // On fai bouger la bullet 

    }
    void HitTarget()
    {
        Destroy(target.gameObject);
        Destroy(gameObject);
    }
}
