using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.SceneScripts
{
    public class PlayRoomScene : MonoBehaviour
    {
        public GameObject[] players;
       
        public GameObject menu;
        //button
        public GameObject[] buttonPanel;
       
        public GameObject result;
        public GameObject menuPanel;
        public GameObject raisePanel;
        public Slider raiseSlider;
        public TextMeshProUGUI raiseText ;
        public Image[] cards;
        public Image[] drawCards;
        public Sprite[] cardSprite;
        public GameObject LoadingText;
        public Image LoadingBar;
        public GameObject drawCard0;
        

        private bool isMenuClick;
        private bool isRaiseClick;
        private int stepCount;
        private float offsetValue = 70;
        private List < Sprite > totalCardImages;
        private string[] files;
        private GameObject[] gameObj;
        private float width;
        private float currentValue;
        private float speed = 5;
        private Vector3 originPos;
        //definition function apply
        public void MenuClick()
        {
            Vector2 newpos = new Vector2(menu.GetComponent<RectTransform>().localPosition.x - menu.GetComponent<RectTransform>().rect.width/2 + menuPanel.GetComponent<RectTransform>().rect.width/2, menu.GetComponent<Image>().rectTransform.localPosition.y *0.65f);
            if (isMenuClick == false)
            {
                
                menuPanel.transform.localPosition = newpos;
                isMenuClick = true;
                buttonPanel[0].GetComponent<Button>().interactable = false;
                buttonPanel[1].GetComponent<Button>().interactable = false;
                buttonPanel[2].GetComponent<Button>().interactable = false;
                buttonPanel[3].GetComponent<Button>().interactable = false;
                buttonPanel[4].GetComponent<Button>().interactable = false;
                result.GetComponent<Button>().interactable = false;
            }
            else
            {
                menuPanel.transform.localPosition = new Vector2(1000, 1000);
                isMenuClick = false;
                buttonPanel[0].GetComponent<Button>().interactable = true;
                buttonPanel[1].GetComponent<Button>().interactable = true;
                buttonPanel[2].GetComponent<Button>().interactable = true;
                buttonPanel[3].GetComponent<Button>().interactable = true;
                buttonPanel[4].GetComponent<Button>().interactable = true;
                result.GetComponent<Button>().interactable = true;
            }
        }

        public void RaiseClick()
        {
            Vector2 newPos = new Vector2(buttonPanel[2].transform.localPosition.x + buttonPanel[2].GetComponent<RectTransform>().rect.width / 2 -
                raisePanel.GetComponent<RectTransform>().rect.width / 2,   -buttonPanel[2].GetComponent<RectTransform>().rect.height / 9 -
                raisePanel.GetComponent<RectTransform>().rect.height );
            if (isRaiseClick == false)
            {
                raisePanel.transform.localPosition = newPos;
                isRaiseClick = true;
                buttonPanel[0].GetComponent<Button>().interactable = false;
                buttonPanel[1].GetComponent<Button>().interactable = false;
                buttonPanel[3].GetComponent<Button>().interactable = false;
                buttonPanel[4].GetComponent<Button>().interactable = false;
                result.GetComponent<Button>().interactable = false;
                menu.GetComponent<Button>().interactable = false;
            }
            else
            {
                raisePanel.transform.localPosition = new Vector2(1000, 1000);
                isRaiseClick = false;
                buttonPanel[0].GetComponent<Button>().interactable = true;
                buttonPanel[1].GetComponent<Button>().interactable = true;
                buttonPanel[3].GetComponent<Button>().interactable = true;
                buttonPanel[4].GetComponent<Button>().interactable = true;
                menu.GetComponent<Button>().interactable = true;
                result.GetComponent<Button>().interactable = true;
            }
           
        }

        public void RaiseOnValueChanged()
        {
            float value = raiseSlider.value * 100/10;
            int intvalue = (int)value;
           
            if (intvalue == 10)
            {
                raiseText.text = "AllIn";
            }
            else
            raiseText.text = intvalue.ToString();
        }
        //public void Buy
        // Use this for initialization
        void Start()
        {
            float cardWidth = drawCard0.GetComponent<RectTransform>().rect.width;
            float offset = (cardWidth * 5f +40)/2;
            float newPos = players[10].transform.localPosition.x -offset;
             originPos = new Vector3(newPos, 0, 0);
            for (int i = 0; i < 5; i++)
            {
                drawCards[i].rectTransform.localPosition = players[5].transform.localPosition + new Vector3(0,30,0);
            }
            for (int i = 0; i < 20; i++)
            {
                cards[i].rectTransform.localPosition = new Vector2(1000, 1000);
            }
            isMenuClick = false;
            menuPanel.transform.localPosition = new Vector2(1000, 1000);
            raisePanel.transform.localPosition = new Vector2(1000, 1000);
            //cards[0].transform.rotation =Quaternion.AngleAxis(-180 ,Vector3.up);

            //cards[1].transform.rotation = Quaternion.AngleAxis(-180, Vector3.up);
            //string path = @"D:\UnityDevelopment\PokerDevelopment\TexasPoker\Assets\Images\CardImage\club\";

            ////pathPreFix = @"file://";

            //files = System.IO.Directory.GetFiles(path, "*.png");

            ////gameObj = GameObject.FindGameObjectsWithTag("Pics");

           
            width = Screen.width / 7;
            currentValue = 100;

        }

        // Update is called once per frame
        void Update()
        {
            if (stepCount < 20)
            {


                for (int i = 0; i < 20; i++)
                {
                    if (i < 2)
                    {
                        cards[i].rectTransform.localScale = new Vector2(1, 1);
                    }
                    else
                        cards[i].rectTransform.localScale = new Vector2(0.7f, 0.7f);
                    cards[i].rectTransform.localPosition = CardDistributionAnimationPos(players[10].transform.localPosition, players[i / 2].transform.localPosition, i, 20, stepCount);

                    //cards[1].rectTransform.localPosition = CardDistributionAnimationPos(players[5].GetComponent<Image>().rectTransform.localPosition, players[0].GetComponent<Image>().rectTransform.localPosition, 0, 10, stepCount);
                }
                drawCards[0].rectTransform.localPosition = CardDrawAnimationPos(originPos, 40, stepCount);

            }

            if (stepCount < 30)
            {
                int count = stepCount - 10;
                drawCards[1].rectTransform.localPosition = CardDrawAnimationPos(drawCards[0].rectTransform.localPosition, 40, count);
            }
            if (stepCount < 50)
            {
                int count = stepCount - 30;
                drawCards[2].rectTransform.localPosition = CardDrawAnimationPos(drawCards[1].rectTransform.localPosition, 40, count);
            }
            if (stepCount < 70)
            {
                int count = stepCount - 50;
                drawCards[3].rectTransform.localPosition = CardDrawAnimationPos(drawCards[2].rectTransform.localPosition, 40, count);
            }
            if (stepCount < 90)
            {
                int count = stepCount - 70;
                drawCards[4].rectTransform.localPosition = CardDrawAnimationPos(drawCards[3].rectTransform.localPosition, 40, count);
            }
            if (stepCount > 100 && stepCount < 131)
            {
                int count = stepCount - 100;
                for (int i = 0; i < 2; i++)
                {
                    cards[i].transform.rotation = CardRotationAnimation(count);
                    Quaternion quaternion = new Quaternion();
                    if (i % 2 == 0)
                    {
                        quaternion = Quaternion.AngleAxis(5, Vector3.forward);
                    }
                    else
                        quaternion = Quaternion.AngleAxis(-10, Vector3.forward);

                    cards[i].rectTransform.localRotation = quaternion;

                    //cards[1].rectTransform.localRotation = CardRotationAnimation(count);
                }
            }
            if (stepCount == 116)
            {
                for (int i = 0; i < 2; i++)
                {
                    cards[i].sprite = cardSprite[1];
                }
            }
            stepCount++;

            if (currentValue > 0)
            {
                currentValue -= speed * Time.deltaTime;
                //ProgressIndicator.text = ((int)currentValue).ToString() + "%";
                LoadingText.SetActive(true);
            }
            else
            {
                LoadingText.SetActive(false);

            }

            LoadingBar.fillAmount = currentValue / 100;

        }
        private  Vector2 CardDistributionAnimationPos(Vector3 dealerPos, Vector3 playerPos, int playerNum,int stepNum , int stepCount)
        {
            float dealerPosX = dealerPos.x;
            float dealerPosY = dealerPos.y;
            float playerPosX = playerPos.x;
            float playerPosY = playerPos.y;
            float deltaPosX = (dealerPosX - playerPosX) / stepNum;
            float deltaPOsY = (dealerPosY - playerPosY) / stepNum;
            Vector2 result;

            if (playerNum < 2)
            {
                if (playerNum % 2 == 0)
                {
                    Vector2 newPos = new Vector2(dealerPosX + offsetValue/6*5 - deltaPosX * stepCount, dealerPosY - offsetValue / 3 - deltaPOsY * stepCount);
                    result = newPos;
                }
                else
                {
                    Vector2 newPos = new Vector2(dealerPosX + offsetValue/6*5 - deltaPosX * stepCount + 20, dealerPosY - offsetValue / 3 - deltaPOsY * stepCount);
                    result = newPos;

                }
            }
            else if (playerNum < 10)
            {
                if (playerNum % 2 == 0)
                {
                    Vector2 newPos = new Vector2(dealerPosX + offsetValue / 4 - deltaPosX * stepCount, dealerPosY - offsetValue / 3 - deltaPOsY * stepCount);
                    result = newPos;
                }
                else
                {
                    Vector2 newPos = new Vector2(dealerPosX + offsetValue / 4 - deltaPosX * stepCount + 20, dealerPosY - offsetValue / 3 - deltaPOsY * stepCount);
                    result = newPos;

                }
            }


            else
            {
                if (playerNum % 2 == 1)
                {
                    Vector2 newPos = new Vector2(dealerPosX - offsetValue / 2 - deltaPosX * stepCount, dealerPosY - offsetValue / 3 - deltaPOsY * stepCount);
                    result = newPos;
                }
                else
                {
                    Vector2 newPos = new Vector2(dealerPosX - offsetValue / 2 - deltaPosX * stepCount + 20, dealerPosY - offsetValue / 3 - deltaPOsY * stepCount);
                    result = newPos;
                }
            }
            
            
            return result;
        }

        private Quaternion CardRotationAnimation(int count)
        {
            int frameNum = 60;
            Quaternion quaternion = new Quaternion();
            quaternion = Quaternion.AngleAxis(360 / frameNum * count - 180, Vector3.up);
            return quaternion;
        }
        private Vector2 CardDrawAnimationPos(Vector3 PreCardPos, int stepNum, int stepCount)
        {
            float widthOffset = 80;
            float preCardPosX = PreCardPos.x;
            Vector2 result;
            Vector2 newPos = new Vector2(preCardPosX + widthOffset / stepNum * stepCount, 0);
            result = newPos;
            return result;
        }
    }
}
