using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button _singInCanvasButton;
    [SerializeField] private Button _registerCanvasButton;
    
    [SerializeField] private GameObject _signInCanvas;
    [SerializeField] private GameObject _registerCanvas;
    [SerializeField] private GameObject _optionsCanvas;
    
    [SerializeField] private TMP_InputField _usernameInput;
    [SerializeField] private TMP_InputField _passwordInput;
    [SerializeField] private TMP_Text _buttonText;

    private const string AuthGuidKey = "auth_guid_key";
        
        public UnityEvent OnSuccessEvent;
        public UnityEvent OnErrorEvent;
    
        private bool isLogged;
    
        private void Start()
        {
            if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
            {
                PlayFabSettings.staticSettings.TitleId = "131B5";
            }                        
        }      
    
        // Нажатие на кнопку входа
        public void OnTryToLogin()
        {
            var needCreation = PlayerPrefs.HasKey(AuthGuidKey);
            var id = PlayerPrefs.GetString(AuthGuidKey, Guid.NewGuid().ToString());
            
            if (!isLogged)
            {
                var request = new LoginWithCustomIDRequest
                {
                    CustomId = id,
                    CreateAccount = !needCreation
                };
    
                PlayFabClientAPI.LoginWithCustomID(request,
                    result =>
                    {
                        PlayerPrefs.SetString(AuthGuidKey, id);
                        OnLoginSuccess(result);
                    }, OnLoginError);
            }
            
            PhotonNetwork.NickName = _usernameInput.text;
            _buttonText.text = "Connecting...";
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = PhotonNetwork.AppVersion;
        }
    
        // Ивенты различных сценариев
        private void OnLoginSuccess(LoginResult result)
        {
            Debug.Log("Complete Login");
            OnSuccessEvent.Invoke();
            //SetUserData(result.PlayFabId);
        }

        private void GetUserData(string playFabId, string keyData)
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest
            {
                PlayFabId = playFabId
            }, result =>
            {
                if (result.Data.ContainsKey(keyData))
                    Debug.Log($"{keyData}: {result.Data[keyData].Value}");
            }, OnLoginError);
        }
            
        private void OnLoginError(PlayFabError error)
        {
            var errorMessage = error.GenerateErrorReport();
            Debug.LogError(errorMessage);
            OnErrorEvent.Invoke();
        }
        
        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster");
        }

        public void OnSignInCanvasButtonClicked()
        {
            _signInCanvas.SetActive(true);
            HideSignInAndRegisterButtons();
        }
        
        public void OnRegisterCanvasButtonClicked()
        {
            _registerCanvas.SetActive(true);
            HideSignInAndRegisterButtons();
        }

        private void HideSignInAndRegisterButtons()
        {
            _singInCanvasButton.gameObject.SetActive(false);
            _registerCanvasButton.gameObject.SetActive(false);
        }
        
        public void OnOptionsButtonClick()
        {
            _optionsCanvas.SetActive(true);

        }
}
