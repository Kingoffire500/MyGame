using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed; // Permet de gérer la vitesse des ennemis
    public float health = 100;
    public int worth = 50;

    void Start()
    {
        speed = startSpeed;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }
    public void Slow (float pct)
    {
            speed = startSpeed * (1f - pct);
    }

    void Die()
    {
         PlayerStats.Money += worth;
         Destroy(gameObject);
    }
}


