using System.Collections;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    public Transform[] spawnPoints; // Массив точек спавна
    public GameObject enemyPrefab; // Префаб врага
    private int maxEnemies = 20;
    private int minEnemies = 15;
    private bool spawningEnabled = true;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (spawningEnabled)
        {
            if (GameObject.FindGameObjectsWithTag("EnemyRobot").Length < minEnemies)
            {
                Vector3 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
            }

            yield return new WaitForSeconds(3f);

            if (GameObject.FindGameObjectsWithTag("EnemyRobot").Length >= maxEnemies)
            {
                spawningEnabled = false;
            }
        }
    }
}