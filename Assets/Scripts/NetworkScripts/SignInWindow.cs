using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class SignInWindow : AccountDataWindowBase
{
    [SerializeField] private Button _signInButton;
    
    protected override void SubscriptionElementsUi()
    {
        base.SubscriptionElementsUi();
        _signInButton.onClick.AddListener(SignIn);
    }

    private void SignIn()
    {
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest
        {
            Username = _username,
            Password = _password
        }, 
        result => { Debug.Log($"Success: {_username}");
            EnterInGameScene();
        }, 
        error => { Debug.Log($"Fail: {error.ErrorMessage}"); });
    }
}
