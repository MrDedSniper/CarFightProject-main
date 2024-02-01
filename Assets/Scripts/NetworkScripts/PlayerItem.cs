using System;
using ExitGames.Client.Photon;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerItem : MonoBehaviourPunCallbacks
{
   [SerializeField] private TMP_Text _playerName;
   [SerializeField] private Image backgroundImage;
   public Color highlightColor;
   [SerializeField] private GameObject _leftArrowButton;
   [SerializeField] private GameObject _rightArrowButton;

   private Hashtable playerProperties = new Hashtable();
   public Image playerCar;
   public Sprite[] avatars;

   private Player player;

   private void Start()
   {
      backgroundImage = GetComponent<Image>();
   }

   public void SetPlayerInfo(Player _player)
   {
      _playerName.text = _player.NickName;
      player = _player;
      UpdatePlayerItem(player);
   }

   public void ApplyLocalChanges()
   {
      backgroundImage.color = highlightColor;
      _leftArrowButton.SetActive(true);
      _rightArrowButton.SetActive(true);
   }

   public void OnClickLeftArrow()
   {
      if ((int)playerProperties["playerCar"] == 0)
      {
         playerProperties["playerCar"] = avatars.Length - 1;
      }
      
      else
      {
         playerProperties["playerCar"] = (int)playerProperties["playerCar"] - 1;
      }
      
      PhotonNetwork.SetPlayerCustomProperties(playerProperties);
   }
   
   public void OnClickRightArrow()
   {
      if ((int)playerProperties["playerCar"] == -1)
      {
         playerProperties["playerCar"] = 0;
      }
      
      else
      {
         playerProperties["playerCar"] = (int)playerProperties["playerCar"] + 1;
      }

      PhotonNetwork.SetPlayerCustomProperties(playerProperties);
   }

   public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable playerProperties)
   {
      if (player == targetPlayer)
      {
         UpdatePlayerItem(targetPlayer);
      }
   }

   private void UpdatePlayerItem(Player targetPlayer)
   {
      if (player.CustomProperties.ContainsKey("playerCar"))
      {
         int carIndex = (int) player.CustomProperties["playerCar"];
         if (carIndex >= 0 && carIndex < avatars.Length)
         {
            playerCar.sprite = avatars[carIndex];
            playerProperties["playerCar"] = carIndex;
         }
         else
         {
            playerProperties["playerCar"] = 0;
         }
      }
      else
      {
         playerProperties["playerCar"] = 0;
      }
   }
}
