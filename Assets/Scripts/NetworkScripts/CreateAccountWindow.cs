using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class CreateAccountWindow : AccountDataWindowBase
{
    [SerializeField] private Button _createAccountButton;

    private string _mail;
    
    protected override void SubscriptionElementsUi()
    {
        base.SubscriptionElementsUi();
        _createAccountButton.onClick.AddListener(CreateAccount);
    }

    private void CreateAccount()
    {
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest
            {
                Username = _username,
                Password = _password,
                RequireBothUsernameAndEmail = false
            }, 
            result => { Debug.Log($"Success: {_username}");
                EnterInGameScene();
            }, 
            error => { Debug.Log($"Fail: {error.ErrorMessage}"); });
    }

}
