using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using Photon.Pun;

internal class PlayerHealth : MonoBehaviour
{
    private PlayerItem _playerItem;
    
    internal float _currentHealth;
    internal float _maxHealth;

    [SerializeField] private HealthbarBevahior _healthbarBevahior;
    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        _playerItem = FindObjectOfType<PlayerItem>();
        GetMaxHealth();
    }

    private void GetMaxHealth()
    {
        int carIndex = (int) PhotonNetwork.LocalPlayer.CustomProperties["playerCar"];
        if (carIndex < 5)
        {
            _maxHealth = 100f;
        }
        else if (carIndex == 5)
        {
            _maxHealth = 200f;
        }
        Debug.Log(_maxHealth);
        _currentHealth = _maxHealth;
        
        
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                { "HP", _currentHealth.ToString() },
            },
        }, result => Debug.Log("HP updated successfully"), error => Debug.LogError(error.GenerateErrorReport()));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DamagingEnvironment"))
        {
            TakeDamage(5f);
        }
    }

    internal void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _healthbarBevahior.UpdateHealtbar(_currentHealth, _maxHealth);

        if (_currentHealth <= 0)
        {
            //Die();
        }
        
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable()
            {
                {"HP", _currentHealth.ToString()}
                
            });
        
        
        /*PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                { "HP", _currentHealth.ToString() },
            },
        }, result => Debug.Log("HP updated successfully"), error => Debug.LogError(error.GenerateErrorReport()));*/
        
        photonView.RPC("UpdateHealth", RpcTarget.Others, _currentHealth);

    }
    
    [PunRPC] internal void UpdateHealth(float health)
    {
        _currentHealth = health;
        _healthbarBevahior.UpdateHealtbar(_currentHealth, _maxHealth);
    }

    private void Die()
    {
        throw new System.NotImplementedException();
    }
}
