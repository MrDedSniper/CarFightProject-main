using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class AmmoCrateSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject _ammoCratePrefab;
    private int maxCrates = 3;
    private int minCrates = 1;
    private bool spawningEnabled = true;

    private void Start()
    {
        
        StartCoroutine(SpawnAmmoCrates());
    }
    
    IEnumerator SpawnAmmoCrates()
    {
        while (spawningEnabled)
        {
            if (GameObject.FindGameObjectsWithTag("AmmoCrate").Length < minCrates)
            {
                Vector3 spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
                Instantiate(_ammoCratePrefab, spawnPoint, Quaternion.identity);
            }

            yield return new WaitForSeconds(1f);

            if (GameObject.FindGameObjectsWithTag("AmmoCrate").Length >= maxCrates)
            {
                spawningEnabled = false;
            }
        }
    }
}
