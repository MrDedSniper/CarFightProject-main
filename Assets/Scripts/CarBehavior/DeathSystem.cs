using System;
using System.Collections;
using UnityEngine;

public class DeathSystem : MonoBehaviour
{
    [SerializeField] private PlayerSpawner _playerSpawner;
    private PlayerHealth _playerHealth;
    private CarController _carController;

    private void Start()
    {
        _playerHealth = FindObjectOfType<PlayerHealth>();
        _carController = FindObjectOfType<CarController>();
    }

    internal void StartDeathAnimation()
    {
        _carController.enabled = false;
        
        Rigidbody playerRigidbody = _playerSpawner.GetCurrentPlayerRigidbody();
        
        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.AddForce(Vector3.up * 100f, ForceMode.Impulse);
        }
        
        StartCoroutine(RespawnAfterDelay(3f));
    }
    
    private IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RespawnPlayer();
    }

    internal void RespawnPlayer()
    {
        _playerSpawner.SetCurrentPrefab(null);
        _playerSpawner.SpawnCurrentPlayer();
        _playerHealth.GetMaxHealth();
    }
}
