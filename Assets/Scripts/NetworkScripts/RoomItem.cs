using TMPro;
using UnityEngine;

public class RoomItem : MonoBehaviour
{
    public TMP_Text roomName;
    private LobbyManager _manager;

    private void Start()
    {
        _manager = FindObjectOfType<LobbyManager>();
    }

    public void SetRoomName(string RoomName)
    {
        roomName.text = RoomName;
    }

    public void OnClickItem()
    {
        _manager.JoinRoom(roomName.text);
    }
}
