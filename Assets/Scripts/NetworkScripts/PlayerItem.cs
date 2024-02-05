using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public interface IObjectFinder
{
   LobbyManager FindLobbyManager();
}
public class PlayerItem : MonoBehaviourPunCallbacks
{
   [SerializeField] private TMP_Text _playerName;
   [SerializeField] private Image backgroundImage;
   public Color highlightColor;
   [SerializeField] private GameObject _leftArrowButton;
   [SerializeField] private GameObject _rightArrowButton;

   private LobbyManager _lobbyManager;

   private Hashtable playerProperties = new Hashtable();
   public Image playerCar;
   public List<Sprite> avatars = new List<Sprite>();

   private Player player;

   bool isRCItemInInventory = false;

   public LobbyManager FindLobbyManager()
   {
      if (_lobbyManager == null)
      {
         _lobbyManager = FindObjectOfType<LobbyManager>();
      }
      return _lobbyManager;
   }
   
   private void Start()
   {
      backgroundImage = GetComponent<Image>();
      playerProperties = PhotonNetwork.LocalPlayer.CustomProperties;
    
      CheckForRCItemInInventory();

      // Устанавливаем 0 индекс аватара при создании объекта
      if (!playerProperties.ContainsKey("playerCar"))
      {
         playerProperties["playerCar"] = 0;
         PhotonNetwork.SetPlayerCustomProperties(playerProperties);
         UpdatePlayerCarImage();
      }
   }

   private void CheckForRCItemInInventory()
   {
      PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetInventorySuccess, OnGetInventoryFailure);
   }

   private void OnGetInventorySuccess(GetUserInventoryResult result)
   {
      isRCItemInInventory = result.Inventory.Exists(item => item.ItemId == "RC");

      if (isRCItemInInventory)
      {
         Debug.Log("RC item found in inventory!");
      }
      else
      {
         Debug.Log("RC item not found in inventory.");
         avatars.RemoveAt(5);
      }
   }

   private void OnGetInventoryFailure(PlayFabError error)
   {
      Debug.Log("Failed to get inventory: " + error.ErrorMessage);
   }

   public void SetPlayerInfo(Player _player)
   {
      _playerName.text = _player.NickName;
      player = _player;
   }

   public void ApplyLocalChanges()
   {
      backgroundImage.color = highlightColor;
      _leftArrowButton.SetActive(true);
      _rightArrowButton.SetActive(true);
   }

   public void OnClickLeftArrow()
   {
      if (playerProperties.ContainsKey("playerCar"))
      {
         int currentCarIndex = (int)playerProperties["playerCar"];
         playerProperties["playerCar"] = (currentCarIndex - 1 + avatars.Count) % avatars.Count;
         PhotonNetwork.SetPlayerCustomProperties(playerProperties);

         UpdatePlayerCarImage();
      }
   }

   public void OnClickRightArrow()
   {
      if (playerProperties.ContainsKey("playerCar"))
      {
         int currentCarIndex = (int)playerProperties["playerCar"];
         playerProperties["playerCar"] = (currentCarIndex + 1) % avatars.Count;
         PhotonNetwork.SetPlayerCustomProperties(playerProperties);

         UpdatePlayerCarImage();
      }
   }

   private void UpdatePlayerCarImage()
   {
      if (playerProperties.ContainsKey("playerCar"))
      {
         int carIndex = (int)playerProperties["playerCar"];
         playerCar.sprite = avatars[carIndex];
      }
   }
}
