using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Assets.Scripts.SceneScripts
{
    public class RoomScene : MonoBehaviour
    {
        public GameObject mainCanvas;
        public GameObject createCanvas;
        public GameObject roomListCanvas;
        public GameObject panel;
        public GameObject[] rooms;
        public GameObject roomName;
        public GameObject sbInfo;
        public GameObject entrySlider;
        public GameObject membersSlider;
        public GameObject startmembersSlider;

        private bool dragging;
        private float screenX;
        private float screenY;
      
        public void CreateRoomClick()
        {
            Vector2 newPos = new Vector2(1000, 1000);
            mainCanvas.transform.localPosition = newPos;
            createCanvas.transform.localPosition = new Vector2(0, 0);
            roomListCanvas.transform.localPosition = newPos;

            roomName.GetComponent<TMP_InputField>().text = "";
            sbInfo.GetComponent<TMP_InputField>().text = "";
            entrySlider.GetComponent<Slider>().value = 0;
            membersSlider.GetComponent<Slider>().value = 0;
            startmembersSlider.GetComponent<Slider>().value = 0;
        }
        public void JoinRoomClick()
        {
            Vector2 newPos = new Vector2(1000, 1000);
            mainCanvas.transform.localPosition = newPos;
            createCanvas.transform.localPosition = newPos;
            roomListCanvas.transform.localPosition = new Vector2(0, 0);
        }
        public void ReturnCreateRoomClick()
        {
            Vector2 newPos = new Vector2(1000, 1000);
            mainCanvas.transform.localPosition = new Vector2(0, 0); ;
            createCanvas.transform.localPosition = newPos;
            roomListCanvas.transform.localPosition = newPos;
        }
        public void ReturnListRoomClick()
        {
            Vector2 newPos = new Vector2(1000, 1000);
            mainCanvas.transform.localPosition = new Vector2(0, 0); ;
            createCanvas.transform.localPosition = newPos;
            roomListCanvas.transform.localPosition = newPos;
        }
        
        public void BeginDrag()
        {
            dragging = true;
        }
        public void EndDrag()
        {
            dragging = false;
        }
       

        // Use this for initialization
        void Start()
        {
            Vector2 newPos = new Vector2(1000, 1000);
            dragging = false;
            screenX = Screen.width;
            screenY = Screen.height;
            mainCanvas.transform.localPosition = new Vector2(0, 0); ;
            createCanvas.transform.localPosition = newPos;
            roomListCanvas.transform.localPosition = newPos;
           
        }

        // Update is called once per frame
        void Update()
        {

          
        }
       
    }
}
