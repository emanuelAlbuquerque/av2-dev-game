using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEngine : MonoBehaviour
{
    public GameObject enemy; 
    public float spawnInterval = 5f;
    public Vector3 spawnAreaSize;
    public Vector3 spawnAreaCenter;
    public Player player; 
    void Start()
    {
        InvokeRepeating("CriaInimigo", spawnInterval, spawnInterval);
    }

     void CriaInimigo()
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

    void FinalizarJogo(){
		  CancelInvoke("CriaInimigo"); 
	}

    public void Retry(){
        SceneManager.LoadScene("demo");
    }

    public void Menu(){
        SceneManager.LoadScene("Menu");
    } 
}
