using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;
    private List<RoomJoiner> rooms = new List<RoomJoiner>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        print("Connecting to server...");
        PhotonNetwork.ConnectUsingSettings("0.0.0");
    }

    public void OnClickCreateRoom()
    {
        print("Connected to master.");
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
        PhotonNetwork.CreateRoom("Leopard", roomOptions, TypedLobby.Default);
    }

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    private void OnJoinedLobby()
    {
        print("Joined lobby.");
    }

    private void OnReceivedRoomListUpdate()
    {
        foreach (RoomJoiner roomButton in rooms)
        {
            Destroy(roomButton.gameObject);
        }
        rooms.Clear();
        int i = 0;
        foreach (RoomInfo room in PhotonNetwork.GetRoomList())
        {
            if (!room.open)
                continue;
            GameObject buttonPrefab = Resources.Load<GameObject>("GUI/RoomGUI");
            GameObject roomButton = Instantiate<GameObject>(buttonPrefab);
            //roomButton.GetComponent<RoomJoiner>().RoomName = "leopard";
            string info = room.name.Trim() + " (" + room.playerCount + "/" + room.maxPlayers + ")";
            print(info);
            roomButton.GetComponentInChildren<Text>().text = info;
            rooms.Add(roomButton.GetComponent<RoomJoiner>());            
            roomButton.transform.position.Set(0, i * 120, 0);
        }
    }

    private void OnJoinedRoom()
    {
        print("Joined player.");
    }
}
