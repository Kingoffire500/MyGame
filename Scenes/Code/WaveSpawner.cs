using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : MonoBehaviour
{

    public static int EnemiesAlive = 0;

    public Wave[] waves;
    
    public Transform spawnPoint; // Position du point de spawn

    public TextMeshProUGUI waveCountdownText;

    public float timeBetweenWaves = 5f; //temps entre chaque vague d'ennemis
    public static float countdown = 2f; //temps de spawn au départ
    private int waveIndex = 0; // Index permettant de savoir le numéro de vague

    public GameManager gameManager;

    void Update()
    {

        if (EnemiesAlive > 0)
        {
            return;
        }
        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();

            this.enabled = false;
            
        }
        // Création de la condition pour faire spawn une vague
        if (countdown <= 0f)
        {
            
            StartCoroutine(SpawnWave()); //Permet de mettre en place une pause au niveau du "yield" sinon les ennemies vont venir se superposer à chaque nouvelles vagues générant trop d'ennemis
            countdown = timeBetweenWaves;// On setup le nouveau temps choisi
            return;
        }
        
        countdown -= Time.deltaTime; //On vient diminuer le countdown de 1 chaque seconde
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity); // Permet d'eviter que le timer aille en dessous de 0 
        waveCountdownText.text = string.Format("{0:00.00}", countdown);  // Permet un affichage du temps entre chaque vague dans l'UI
    }

    // Fonction permettant de faire spawn des vagues 
    IEnumerator SpawnWave() // IEnumerator va de pair avec la fonction SatrtCoroutine
    {
        
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        EnemiesAlive=wave.count;

        for (int i = 0; i < wave.count; i++)// Dans la conditions, on vient dire de faire spawn un ennemis jusqu'a numéro de la vague
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f/wave.rate); //Permet de définir le temps de spawn entre chaque ennemis
        }
        waveIndex++; //Augmente de 1 le numéro de la vague
        
        
    }
    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy,spawnPoint.position,spawnPoint.rotation);// Création d'un ennemi au point de spawn 
    }
}
