using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab; // Position de l'ennemi
    public Transform spawnPoint; // Position du point de spawn

    public TextMeshProUGUI waveCountdownText;

    public float timeBetweenWaves = 5f; //temps entre chaque vague d'ennemis
    private float countdown = 2f; //temps de spawn au d�part
    private int waveIndex = 0; // Index permettant de savoir le num�ro de vague

    void Update()
    {
        // Cr�ation de la condition pour faire spawn une vague
        if (countdown <= 0)
        {
            StartCoroutine(SpawnWave()); //Permet de mettre en place une pause au niveau du "yield" sinon les ennemies vont venir se superposer � chaque nouvelles vagues g�n�rant trop d'ennemis
            countdown = timeBetweenWaves; // On setup le nouveau temps choisi
        }
        countdown -= Time.deltaTime; //On vient diminuer le countdown de 1 chaque seconde
        waveCountdownText.text = Mathf.Round(countdown).ToString(); // Permet un affichage du temps entre chaque vague dans l'UI
    }

    // Fonction permettant de faire spawn des vagues 
    IEnumerator SpawnWave() // IEnumerator va de pair avec la fonction SatrtCoroutine
    {
        waveIndex++; //Augmente de 1 le num�ro de la vague
        for (int i = 0; i < waveIndex; i++)// Dans la conditions, on vient dire de faire spawn un ennemis jusqu'a num�ro de la vague
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f); //Permet de d�finir le temps de spawn entre chaque ennemis
        }
        
    }
    void SpawnEnemy()
    {
        Instantiate(enemyPrefab,spawnPoint.position,spawnPoint.rotation);// Cr�ation d'un ennemi au point de spawn 
    }
}
