using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class ScrapCurrency : MonoBehaviour
{
    private const string scrapCurrency = "SC";
    internal int _currentScrap;

    [SerializeField] private ScrapUI _scrapUI;
    private bool _addedTestCurrency = false;

    private void Start()
    {
        GetCurrency();
    }

    private void GetCurrency()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetInventorySuccess, OnGetInventoryFailure);
    }
    
    void OnGetInventorySuccess(GetUserInventoryResult result)
    {
        _currentScrap = result.VirtualCurrency[scrapCurrency];
        Debug.Log(_currentScrap);

        if (_currentScrap < 1000 && !_addedTestCurrency)
        {
            _addedTestCurrency = true;
            AddCurrencyForTest();
        }
        else
        {
            GetCurrency();
            _scrapUI.UpdateScrapValue();
        }
    }

    private void AddCurrencyForTest()
    {
        PlayFabClientAPI.AddUserVirtualCurrency(new AddUserVirtualCurrencyRequest
        {
            VirtualCurrency = scrapCurrency,
            Amount = 1000
        }, OnAddCurrencySuccess, OnAddCurrencyFailure);
    }

    void OnGetInventoryFailure(PlayFabError error)
    {
        Debug.LogError("Error getting inventory: " + error.GenerateErrorReport());
    }

    void OnAddCurrencySuccess(ModifyUserVirtualCurrencyResult result)
    {
        Debug.Log("Added 1000 Scrap to inventory");
        SaveCurrency();
        GetCurrency();
        _scrapUI.UpdateScrapValue();
    }

    void OnAddCurrencyFailure(PlayFabError error)
    {
        Debug.LogError("Error adding currency: " + error.GenerateErrorReport());
    }

    void SaveCurrency()
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { scrapCurrency, "1000" }
            }
        }, OnSaveDataSuccess, OnSaveDataFailure);
    }

    void OnSaveDataSuccess(UpdateUserDataResult result)
    {
        Debug.Log("Saved currency data to Title Data");
    }

    void OnSaveDataFailure(PlayFabError error)
    {
        Debug.LogError("Error saving data: " + error.GenerateErrorReport());
    }
}
