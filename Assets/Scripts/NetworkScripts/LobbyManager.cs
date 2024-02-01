using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private WarningsScripts _warningsScripts;
    
    [SerializeField] private TMP_InputField _roomCreationInput;
    [SerializeField] private TMP_InputField _roomSearchInput;
    [SerializeField] private GameObject _lobbyPanel;
    [SerializeField] private GameObject _roomPanel;
    [SerializeField] private TMP_Text _roomName;
    [SerializeField] private TMP_Text _privateRoomText;

    public RoomItem roomItemPrefab;
    private List<RoomItem> roomItemsList = new List<RoomItem>();
    public Transform contentObject;

    public float timeBetweenUpdates = 1.5f;
    private float _nextUpdateTime;

    public List<PlayerItem> playerItemsList = new List<PlayerItem>();
    private Dictionary<string, GameObject> roomListEntries = new Dictionary<string, GameObject>();
    public PlayerItem playerItemPrefab;
    public Transform playerItemParent;

    public GameObject playButton;
    public GameObject closeRoomButton;
    public GameObject copyRoomNameButton;

    [SerializeField] private Toggle _isPrivateRoom;
    string buff = "";

    private void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    //Создание комнаты

    public void OnClickCreate()
    {
        if (_roomCreationInput.text.Length >= 1)
        {
            RoomOptions roomOptions = new RoomOptions()
            {
                MaxPlayers = 4,
                BroadcastPropsChangeToAll = true,
                IsVisible = true,
                IsOpen = !_isPrivateRoom.isOn,
            };

            PhotonNetwork.CreateRoom(_roomCreationInput.text, roomOptions);
        }
    }
    
    public override void OnJoinedRoom()
    {
        if (!PhotonNetwork.CurrentRoom.IsOpen && _isPrivateRoom.isOn)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
        
        _lobbyPanel.SetActive(false);
        _roomPanel.SetActive(true);
        _roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
        
        if (!PhotonNetwork.CurrentRoom.IsOpen)
        {
            _privateRoomText.gameObject.SetActive(true);
            copyRoomNameButton.gameObject.SetActive(true);
            PhotonNetwork.CurrentRoom.IsOpen = false;
            closeRoomButton.GetComponentInChildren<TMP_Text>().text = "Open Room";
            playButton.GetComponent<Button>().interactable = true;
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time >= _nextUpdateTime)
        {
            UpdateRoomList(roomList);
            _nextUpdateTime = Time.time + timeBetweenUpdates;
        }
    }

    private void UpdateRoomList(List<RoomInfo> list)
    {
        foreach (RoomItem item in roomItemsList)
        {
            Destroy(item.gameObject);
        }

        roomItemsList.Clear();
        roomListEntries.Clear();

        foreach (RoomInfo room in list)
        {
            RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
            newRoom.SetRoomName(room.Name);
            roomItemsList.Add(newRoom);

            // Добавляем запись в словарь
            roomListEntries.Add(room.Name, newRoom.gameObject);
            Debug.Log(roomListEntries);

            if (room.IsOpen)
            {
                newRoom.gameObject.SetActive(true);
            }
            else
            {
                newRoom.gameObject.SetActive(false);
            }
        }
    }

    public void SearchRoomByName(string roomName)
    {
        if (string.IsNullOrEmpty(roomName))
        {
            if (roomName.Equals(buff)) return;
            else
            {
                buff = roomName;
                foreach (GameObject entry in roomListEntries.Values)
                {
                    entry.SetActive(true);
                }
            }
        }
        else
        {
            buff = roomName;
            foreach (KeyValuePair<string, GameObject> keyValue in roomListEntries)
            {
                if (keyValue.Key.IndexOf(roomName) != -1)
                    keyValue.Value.SetActive(true);
                else
                    keyValue.Value.SetActive(false);
            }
        }
    



    /*Debug.Log("SearchRoomByName");
    
    foreach (RoomItem item in roomItemsList)
    {
        if (item.roomName.text != _roomSearchInput.text)
        {
            _warningsScripts.CantFindRoomWarning();
            Debug.Log("CantFindRoomWarning");
        }

        else if (item.roomName.text == _roomSearchInput.text)
        {
            Debug.Log("Find!");
            PhotonNetwork.JoinRoom(roomName);
            break;
        }
    }*/
        
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        _roomPanel.SetActive(false);
        _lobbyPanel.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    private void UpdatePlayerList()
    {
        foreach (PlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }

        playerItemsList.Clear();

        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);

            if (player.Value == PhotonNetwork.LocalPlayer)
            {
                newPlayerItem.ApplyLocalChanges();
            }

            playerItemsList.Add(newPlayerItem);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient /*&& PhotonNetwork.CurrentRoom.PlayerCount >= 2*/)
        {
            playButton.SetActive(true);
            closeRoomButton.SetActive(true);
        }

        else
        {
            playButton.SetActive(false);
            closeRoomButton.SetActive(false);
        }
    }

    public void OnClickCloseButton()
    {
        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        if (PhotonNetwork.CurrentRoom.IsOpen)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            _privateRoomText.gameObject.SetActive(true);
            copyRoomNameButton.gameObject.SetActive(true);
            closeRoomButton.GetComponentInChildren<TMP_Text>().text = "Open Room";
            playButton.GetComponent<Button>().interactable = true;
            Hashtable properties = new Hashtable();
            properties.Add("isOpen", false);
            PhotonNetwork.CurrentRoom.SetCustomProperties(properties);
        }
        else
        {
            PhotonNetwork.CurrentRoom.IsOpen = true;
            _privateRoomText.gameObject.SetActive(false);
            copyRoomNameButton.gameObject.SetActive(false);
            closeRoomButton.GetComponentInChildren<TMP_Text>().text = "Close Room";
            playButton.GetComponent<Button>().interactable = false;
            Hashtable properties = new Hashtable();
            properties.Add("isOpen", true);
            PhotonNetwork.CurrentRoom.SetCustomProperties(properties);
        }
    }

    public void OnClickPlayButton()
    {
        if (!PhotonNetwork.CurrentRoom.IsOpen)
        {
            PhotonNetwork.LoadLevel("FirstArena");
        }
    }
    
    public void CopyNameOfPrivateRoom()
    {
        GUIUtility.systemCopyBuffer = PhotonNetwork.CurrentRoom.Name;
    }
}