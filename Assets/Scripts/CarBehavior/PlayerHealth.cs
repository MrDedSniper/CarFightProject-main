using System;
using System.Collections.Generic;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using Photon.Pun;

internal class PlayerHealth : MonoBehaviour
{
    internal float _currentHealth;
    internal float _maxHealth = 100f;

    [SerializeField] private HealthbarBevahior _healthbarBevahior;
    private PhotonView photonView;

    void Start()
    {
        PhotonView photonView = GetComponent<PhotonView>();
        
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
        
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                { "HP", _currentHealth.ToString() },
            },
        }, result => Debug.Log("HP updated successfully"), error => Debug.LogError(error.GenerateErrorReport()));
        
        photonView.RPC("UpdateHealth", RpcTarget.Others, _currentHealth);

    }
    
    [PunRPC] void UpdateHealth(float health)
    {
        _currentHealth = health;
        _healthbarBevahior.UpdateHealtbar(_currentHealth, _maxHealth);
    }

    private void Die()
    {
        throw new System.NotImplementedException();
    }
}
