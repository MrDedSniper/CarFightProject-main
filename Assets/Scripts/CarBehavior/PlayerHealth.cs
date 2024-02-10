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
    private HealthbarOnScene _healthbarOnScene;
    private CarController _carController;
    private DeathSystem _deathSystem;
    private UIHealthbar _uiHealthbar;
    
    private PlayerItem _playerItem;
    internal float _currentHealth;
    internal float _maxHealth;

    
    private PhotonView photonView;

    private void Start()
    {
        _deathSystem = FindObjectOfType<DeathSystem>();
        _carController = FindObjectOfType<CarController>();
        _uiHealthbar = FindObjectOfType<UIHealthbar>();
        _healthbarOnScene = FindObjectOfType<HealthbarOnScene>();
        
        photonView = GetComponent<PhotonView>();
        _playerItem = FindObjectOfType<PlayerItem>();
        GetMaxHealth();
    }

    internal void GetMaxHealth()
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DamagingEnvironment"))
        {
            float speed = _carController._currentSpeed;
            float damage = CalculateDamage(speed);
            TakeDamage(damage);
        }
    }
    
    private float CalculateDamage(float speed)
    {
        float damagePer100KmH = 40f;
        float damage = (speed / 100f) * damagePer100KmH;
        return damage;
    }

    internal void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _healthbarOnScene.UpdateHealtbar(_currentHealth, _maxHealth);

        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable()
            {
                {"HP", _currentHealth.ToString()}
                
            });
        
        photonView.RPC("UpdateHealth", RpcTarget.Others, _currentHealth);
        
        if (_currentHealth <= 0)
        {
            Die();
        }

        _uiHealthbar.UpdateHealth();

    }
    
    [PunRPC] internal void UpdateHealth(float health)
    {
        _currentHealth = health;
        _healthbarOnScene.UpdateHealtbar(_currentHealth, _maxHealth);
    }

    private void Die()
    {
        Debug.Log("Die");
        _deathSystem.StartDeathAnimation();
    }
}
