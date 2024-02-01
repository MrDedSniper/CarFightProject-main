using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabLogin : MonoBehaviour
{
    /*[SerializeField] private string _username;
    
    private void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "131B5";
        }
    }

    private bool IsValidUsername()
    {
        bool isValid = false;
        
        if (_username.Length >= 2 || _username.Length <= 24)
        {
            isValid = true;
        }

        return isValid;
    }

    private void LoginWithCustomId()
    {
        Debug.Log($"Login Playfab as {_username}");
        var request = new LoginWithCustomIDRequest {CustomId = _username, CreateAccount = true};
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginCustomIdSeccess, OnFailure);
    }

    private void OnLoginCustomIdSeccess(LoginResult result)
    {
        Debug.Log("Custom ID worked");
    }
    
    private void OnFailure(PlayFabError obj)
    {
        Debug.Log("OnFailure");
    }

    public void SetUsername(string error)
    {
        _username = name;
        PlayerPrefs.SetString("Username", _username);
    }

    public void Login()
    {
        if(!IsValidUsername()) return;

        LoginWithCustomId();
    }*/
}

