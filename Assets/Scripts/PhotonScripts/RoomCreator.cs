using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.SceneScripts;
using PlayFab;
using PlayFab.Json;
using PlayFab.ClientModels;

public class RoomCreator : MonoBehaviour
{
    public static RoomCreator Instance;
    private List<RoomJoiner> rooms = new List<RoomJoiner>();
    public GameObject roomPrefab;
    public GameObject roomFrame;
    public string roomNameInfo;
    public int roomIndex = 0;
    public PhotonPlayer playerMe;
    public bool[] isPlayer = new bool[10] {false, false, false, false, false, false, false, false, false, false };

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        roomIndex = 0;

        foreach (RoomInfo room in PhotonNetwork.GetRoomList())
        {
            if (!room.open)
                continue;

            int indexLength;
            GameObject clone;
            clone = Instantiate(roomPrefab, roomPrefab.transform.position,
                roomPrefab.transform.rotation, roomPrefab.transform.parent) as GameObject;
            clone.transform.localPosition = new Vector2(350 * roomIndex, 0);
            clone.GetComponent<RoomJoiner>().roomId = room.name;
            Text[] roomItem = roomPrefab.GetComponentsInChildren<Text>();
            string info = room.name.Trim() + " (" + room.playerCount + "/" + room.maxPlayers + ")";
            print(info);
            roomItem[0].text = info;

            if (roomIndex > 6)
            {
                indexLength = roomIndex - 6;
                Vector2 newPos = new Vector2(roomFrame.GetComponent<RectTransform>().sizeDelta.x, roomFrame.GetComponent<RectTransform>().sizeDelta.y + 60 * indexLength);

                roomFrame.GetComponent<RectTransform>().sizeDelta = newPos;
            }

            roomIndex++;
        }
    }

    public void OnClickCreateRoom()
    {
        print("Connected to master.");
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
        PhotonNetwork.CreateRoom(RoomCreate.Instance.roomName.GetComponent<TMP_InputField>().text, roomOptions, TypedLobby.Default);
        //StartCloudGetPlayer();
    }

    private void OnJoinedRoom()
    {
        playerMe = new PhotonPlayer(true, PhotonNetwork.room.playerCount - 1, PlayFabAuthenticator.Instance.playerName);
        SceneManager.SwitchScene("PlayRoomScene");
        print("Joined player.");
    }

    public void StartCloudGetPlayer()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "getPlayer", // Arbitrary function name (must exist in your uploaded cloud.js file)
            GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
        }, OnCloudHelloWorld, OnErrorShared);
    }
    
    //// Build the request object and access the API
    //public void StartCloudHelloWorld()
    //{
    //    PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
    //    {
    //        FunctionName = "helloWorld", // Arbitrary function name (must exist in your uploaded cloud.js file)
    //        FunctionParameter = new { name = "YOUR NAME" }, // The parameter provided to your function
    //        GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
    //    }, OnCloudHelloWorld, OnErrorShared);
    //}

    private static void OnCloudHelloWorld(ExecuteCloudScriptResult result)
    {
        // Cloud Script returns arbitrary results, so you have to evaluate them one step and one parameter at a time
        Debug.Log(JsonWrapper.SerializeObject(result.FunctionResult));
        JsonObject jsonResult = (JsonObject)result.FunctionResult;
        object messageValue;
        jsonResult.TryGetValue("messageValue", out messageValue); // note how "messageValue" directly corresponds to the JSON values set in Cloud Script
        Debug.Log((string)messageValue);
    }

    private static void OnErrorShared(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
}
