using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.SceneScripts;
public class RoomCreate : MonoBehaviour {
    public static RoomCreate Instance;
    public GameObject mainCanvas;
    public GameObject createCanvas;
    public GameObject roomListCanvas;
    public GameObject roomName;
    public GameObject sbInfo;
    public GameObject entryRoomInfo;
    public GameObject initialMembers;
    public GameObject startMemebers;
    public GameObject entrySlider;
    public GameObject membersSlider;
    public GameObject startmembersSlider;
    public GameObject roomPrefab;
    public GameObject roomFrame ;
     
    private List<string>  roomInfo;
    private int roomIntValue;
    private float myheight = 0;
    private int value=0;

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

    public void CreateRoomClick()
    {
        Vector2 newPos = new Vector2(1000, 1000);
        mainCanvas.transform.localPosition = newPos;
        createCanvas.transform.localPosition = newPos;
        roomListCanvas.transform.localPosition = new Vector2(0, 0);
        roomInfo.Add(roomName.GetComponent<TMP_InputField>().text);
        roomInfo.Add(sbInfo.GetComponent<TMP_InputField>().text);
        roomInfo.Add(entryRoomInfo.GetComponent<TextMeshProUGUI>().text);
        roomInfo.Add(initialMembers.GetComponent<TextMeshProUGUI>().text);
        roomInfo.Add(startMemebers.GetComponent<TextMeshProUGUI>().text);

        ////RoomClone(value);

        //value++;
        //Debug.Log(value);
        //SceneManager.SwitchScene("playRoomScene");
    }
    public void EntrySalaryValueChanged()
    {
        float value = entrySlider.GetComponent<Slider>().value * 1000 / 10;
        int intvalue = (int)value;
        entryRoomInfo.GetComponent<TextMeshProUGUI>().text = intvalue.ToString();


    }
    public void RoomMembersValueChanged()
    {
        float value = membersSlider.GetComponent<Slider>().value * 100 / 10;
        roomIntValue = (int)value;
        initialMembers.GetComponent<TextMeshProUGUI>().text = roomIntValue.ToString();
        startmembersSlider.GetComponent<Slider>().value = 0;
    }
    public void StartingMembersValueChanged()
    {
        float value = startmembersSlider.GetComponent<Slider>().value * 100 / 10;
        int intvalue = (int)value;
        if (roomIntValue >= intvalue )
        {
            
            startMemebers.GetComponent<TextMeshProUGUI>().text = intvalue.ToString();
            
        }
    }
    public void ResetClick()
    {
        roomName.GetComponent<TMP_InputField>().text = "";
        sbInfo.GetComponent<TMP_InputField>().text = "";
        entrySlider.GetComponent<Slider>().value = 0;
        membersSlider.GetComponent<Slider>().value = 0;
        startmembersSlider.GetComponent<Slider>().value = 0;
    }
    private void RoomClone(int roomIndex)
    {
        GameObject clone;
        int indexLength;
        clone = Instantiate(roomPrefab, roomPrefab.transform.position,
            roomPrefab.transform.rotation, roomPrefab.transform.parent) as GameObject;
        clone.transform.localPosition = new Vector2(350 * roomIndex, 0);
        Text[] roomItem =  roomPrefab.GetComponentsInChildren<Text>();
        roomItem[0].text = roomInfo[0];
        roomItem[1].text = roomInfo[1];
        roomItem[2].text = roomInfo[2];
        roomItem[3].text = roomInfo[3];
        roomItem[4].text = roomInfo[4];
     
        if (roomIndex > 6)
        {
            indexLength = roomIndex - 6;
            Vector2 newPos = new Vector2(roomFrame.GetComponent<RectTransform>().sizeDelta.x, roomFrame.GetComponent<RectTransform>().sizeDelta.y + 60 * indexLength);

            roomFrame.GetComponent<RectTransform>().sizeDelta = newPos;
        }
    }
    void Start()
    {
        roomInfo = new List<string>();
        
    }
    void Update()
    {
       
    }
}
