using UnityEngine;
using Photon.Pun;
using Random = UnityEngine.Random;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _playerPrefabs;
    [SerializeField] private Transform[] _spawnPoints;
    internal GameObject _currentPrefab;

    private void Awake()
    {
        SpawnCurrentPlayer();
    }

    public void SpawnCurrentPlayer()
    {
        int randomNumber = Random.Range(0, _spawnPoints.Length);
        Transform spawnPoint = _spawnPoints[randomNumber];

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("playerCar"))
        {
            int carIndex = (int) PhotonNetwork.LocalPlayer.CustomProperties["playerCar"];
            if (carIndex >= 0 && carIndex < _playerPrefabs.Length)
            {
                GameObject playerToSpawn = _playerPrefabs[carIndex];
                PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);
                SetCurrentPrefab(playerToSpawn);
            }
            else
            {
                Debug.LogError("Invalid player car index: " + carIndex);
            }
        }
        else
        {
            Debug.LogError("Player car property not found");
        }
    }
    
    public void SetCurrentPrefab(GameObject prefab)
    {
        _currentPrefab = prefab;
    }
    
    public Rigidbody GetCurrentPlayerRigidbody()
    {
        if (_currentPrefab != null)
        {
            Rigidbody playerRigidbody = _currentPrefab.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                return playerRigidbody;
            }
            else
            {
                Debug.LogError("Rigidbody component not found on current player prefab");
                return null;
            }
        }
        else
        {
            Debug.LogError("Current player prefab is null");
            return null;
        }
    }
}
