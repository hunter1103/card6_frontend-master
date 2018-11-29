using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.SceneScripts;

public class GamePlay : Photon.PunBehaviour
{
    public static GamePlay Instance;
    public PhotonView gameView;
    public Sprite[] Characters;
    public Sprite playerWaitingSprite;    
    public int currentPlayers;
    public List<int> currentSeatIndex;
    public int meSeatIndex;
    public int[] myHandCardIndex = new int[2];
    public List<int> blindPlayerIndex;
    public bool isStart = false;
    public bool isConfirmSeat = false;

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
        gameView = this.GetComponent<PhotonView>();
    }

    void Update()
    {
        currentPlayers = PhotonNetwork.room.playerCount;
        currentSeatIndex = new List<int>();
        blindPlayerIndex = new List<int>();

        // Format player images in real time.
        for (int i = 0; i < 10; i++)
        {
            Image[] playerImageStore = PlayRoomScene.Instance.players[i].GetComponentsInChildren<Image>();
            playerImageStore[1].sprite = playerWaitingSprite;
        }
        
        // Declare my seat number.
        if (isConfirmSeat == false)
        {
            for (int i = 0; i < PhotonNetwork.room.playerCount; i++)
            {
                if (PhotonNetwork.playerList[i].UserId == PhotonNetwork.player.UserId)
                {
                    meSeatIndex = i;
                    //Debug.Log("mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm" + meSeatIndex);
                }
            }

            isConfirmSeat = true;
        }       

        //Confirm our gameplay formation in real time.
        for (int i = 0; i < PhotonNetwork.room.playerCount; i++)
        {
            Image[] playerImageStore = PlayRoomScene.Instance.players[(i + PhotonNetwork.room.maxPlayers - meSeatIndex) % PhotonNetwork.room.maxPlayers].GetComponentsInChildren<Image>();
            //Debug.Log("kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk" + (i + PhotonNetwork.room.maxPlayers - meSeatIndex) % PhotonNetwork.room.maxPlayers);
            currentSeatIndex.Add((i + PhotonNetwork.room.maxPlayers - meSeatIndex) % PhotonNetwork.room.maxPlayers);
            playerImageStore[1].sprite = Characters[i];
        }

        //Confirm our gameplay blind formation in real time.
        for (int i = 0; i < 3; i++)
        {
            blindPlayerIndex.Add((10 - meSeatIndex + i) % 10);
        }
    }

    public void OnClickModel()
    {
        isStart = true;
        myHandCardIndex[0] = Random.Range(0, 52);
        myHandCardIndex[1] = Random.Range(0, 52);
        gameView.RPC("StartGamePlayNow", PhotonTargets.All);
    }

    private void OnLeftRoom()
    {
               
    }

    [PunRPC]
    public void StartGamePlayNow()
    {
        myHandCardIndex[0] = Random.Range(0, 52);
        myHandCardIndex[1] = Random.Range(0, 52);
        isStart = true;
    }
}
