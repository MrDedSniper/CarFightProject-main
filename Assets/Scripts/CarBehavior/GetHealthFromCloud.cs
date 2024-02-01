using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

/*public void GetHealthFromCloud()
{
    var request = new GetUserDataRequest();
    request.Keys = new List<string>() { "health" };

    PlayFabClientAPI.GetUserData(request, result =>
    {
        if (result.Data.ContainsKey("health"))
        {
            health = int.Parse(result.Data["health"].Value);
        }
    }, error => Debug.LogError(error.GenerateErrorReport()));
}*/