using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEngine : MonoBehaviour
{
    public GameObject enemy; 
    public Vector3 spawnAreaSize;
    public Vector3 spawnAreaCenter;
    public Player player; 
    public Text textWave;
    public float initialSpawnInterval = 10f; 
    public float spawnIntervalDecrease = 1f; 
    private int currentWave = 1; 
    private float currentSpawnInterval;

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        StartCoroutine(SpawnWaves());
    }

    void Update(){
        textWave.text = $"Wave: {currentWave.ToString()}";
    }

    public IEnumerator SpawnWaves()
    {
        while (true)
        {
            yield return StartCoroutine(SpawnEnemies());
            yield return new WaitForSeconds(5f); 
            currentWave++;
            currentSpawnInterval = Mathf.Max(1f, initialSpawnInterval - (currentWave - 1) * spawnIntervalDecrease);  // Garante que o intervalo não seja menor que 1 segundo
        }
    }
    public IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < currentWave * 5; i++)  // Número de inimigos por wave, por exemplo, 5 inimigos por wave
        {
            CreateEnemy();
            yield return new WaitForSeconds(currentSpawnInterval);
        }
    }

     void CreateEnemy()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2),
            spawnAreaCenter.y,
            Random.Range(spawnAreaCenter.z - spawnAreaSize.z / 2, spawnAreaCenter.z + spawnAreaSize.z / 2)
        );

        GameObject newEnemy = Instantiate(enemy, randomPosition, Quaternion.identity);

        Enemy enemyScript = newEnemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.player = player;
            enemyScript.playerHealth = player.GetComponent<Health>(); 
        }
    }


    public void Retry(){
        SceneManager.LoadScene("demo");
    }

    public void Menu(){
        SceneManager.LoadScene("Menu");
    } 
}
