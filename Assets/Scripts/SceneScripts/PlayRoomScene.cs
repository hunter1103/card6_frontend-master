using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.SceneScripts
{
    public class PlayRoomScene : MonoBehaviour
    {
        public static PlayRoomScene Instance;
        public GameObject[] players;

        public GameObject menu;
        //button
        public GameObject[] buttonPanel;

        public GameObject result;
        public GameObject menuPanel;
        public GameObject raisePanel;
        public Slider raiseSlider;
        public TextMeshProUGUI raiseText;
        public GameObject[] cards;
        public GameObject[] drawCards;
        public Sprite[] cardSprite;
        public GameObject LoadingText;
        public Image LoadingBar;
        public RectTransform sparkMark;
        public GameObject[] pots;
        public GameObject[] chips;

        private bool isMenuClick;
        private bool isRaiseClick;
        private int stepCount;
        private float offsetValue = 70;
        private List<Sprite> totalCardImages;
        private string[] files;
        private GameObject[] gameObj;
        private float width;
        private float currentValue;
        private float speed = 5;
        private Vector3 originPos;
        public Image[] gamePlayButtons;

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
            StartCoroutine(MoveCards(cards, players[10], players, 0.19f));
            StartCoroutine(DrawCardsAni(drawCards, 0.12f,new Vector3 (-0.8f ,0.35f,0), 0.19f,5));
            PotArrangment();
            StartCoroutine(PotsAni(pots, chips[9].transform.position, 1f));



            //for (int i = 0; i < 5; i++)
            //{
            //    drawCards[i].rectTransform.localPosition = players[5].transform.localPosition + new Vector3(1000, 30, 0);
            //}
            //for (int i = 0; i < 20; i++)
            //{
            //    cards[i].rectTransform.localPosition = new Vector2(1000, 1000);
            //}
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

        void Update()
        {
            if (GamePlay.Instance.isStart == true)
            {
                //CardDistribution();
                BlindButtonDistribution();
                LoadingBar.rectTransform.localPosition = players[GamePlay.Instance.blindPlayerIndex[0]].transform.localPosition;
                MyTurnAnimation();
            }
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
            float angle = currentValue / 100 * 360;
            sparkMark.localEulerAngles = new Vector3(0, 0, -angle);
        }

        //definition function apply
        public void MenuClick()
        {
            Vector2 newpos = new Vector2(menu.GetComponent<RectTransform>().localPosition.x - menu.GetComponent<RectTransform>().rect.width / 2 + menuPanel.GetComponent<RectTransform>().rect.width / 2, menu.GetComponent<Image>().rectTransform.localPosition.y * 0.65f);
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
                raisePanel.GetComponent<RectTransform>().rect.width / 2*0.7f, -buttonPanel[2].GetComponent<RectTransform>().rect.height / 5 -
                raisePanel.GetComponent<RectTransform>().rect.height * 1.23f);
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
            float value = raiseSlider.value * 100 / 10;
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

        // Update is called once per frame        

        public void BlindButtonDistribution()
        {
            if (stepCount < 20)
            {
                for (int i = 0; i < 3; i++)
                {
                    gamePlayButtons[i].rectTransform.localPosition = CardDistributionAnimationPos(players[10].transform.localPosition, players[GamePlay.Instance.blindPlayerIndex[i]].transform.localPosition, i * 2, 20, stepCount);
                }
            }
        }

        //public void CardDistribution()
        //{
        //    if (stepCount < 20)
        //    {
        //        foreach (int i in GamePlay.Instance.currentSeatIndex)
        //        {
        //            if (i == 0)
        //            {
        //                cards[0].rectTransform.localScale = new Vector2(1, 1);
        //                cards[1].rectTransform.localScale = new Vector2(1, 1);                                                
        //            }
        //            else
        //            {
        //                cards[i * 2].rectTransform.localScale = new Vector2(0.7f, 0.7f);
        //                cards[i * 2 + 1].rectTransform.localScale = new Vector2(0.7f, 0.7f);
        //            }

        //            cards[i * 2].rectTransform.localPosition = CardDistributionAnimationPos(players[10].transform.localPosition, players[i].transform.localPosition, i * 2, 20, stepCount);
        //            cards[i * 2 + 1].rectTransform.localPosition = CardDistributionAnimationPos(players[10].transform.localPosition, players[i].transform.localPosition, i * 2 + 1, 20, stepCount);

        //        }

        //        drawCards[0].rectTransform.localPosition = CardDrawAnimationPos(originPos, 40, stepCount);
        //    }

        //    if (stepCount < 30)
        //    {
        //        int count = stepCount - 10;
        //        drawCards[1].rectTransform.localPosition = CardDrawAnimationPos(drawCards[0].rectTransform.localPosition, 40, count);
        //    }
        //    if (stepCount < 50)
        //    {
        //        int count = stepCount - 30;
        //        drawCards[2].rectTransform.localPosition = CardDrawAnimationPos(drawCards[1].rectTransform.localPosition, 40, count);
        //    }
        //    if (stepCount < 70)
        //    {
        //        int count = stepCount - 50;
        //        drawCards[3].rectTransform.localPosition = CardDrawAnimationPos(drawCards[2].rectTransform.localPosition, 40, count);
        //    }
        //    if (stepCount < 90)
        //    {
        //        int count = stepCount - 70;
        //        drawCards[4].rectTransform.localPosition = CardDrawAnimationPos(drawCards[3].rectTransform.localPosition, 40, count);
        //    }
        //    if (stepCount > 100 && stepCount < 131)
        //    {
        //        int count = stepCount - 100;
        //        for (int i = 0; i < 2; i++)
        //        {
        //            cards[i].transform.rotation = CardRotationAnimation(count);
        //            Quaternion quaternion = new Quaternion();
        //            if (i % 2 == 0)
        //            {
        //                quaternion = Quaternion.AngleAxis(5, Vector3.forward);
        //            }
        //            else
        //                quaternion = Quaternion.AngleAxis(-10, Vector3.forward);

        //            cards[i].rectTransform.localRotation = quaternion;

        //            //cards[1].rectTransform.localRotation = CardRotationAnimation(count);
        //        }
        //    }
        //    if (stepCount == 116)
        //    {
        //        for (int i = 0; i < 2; i++)
        //        {
        //            cards[i].sprite = cardSprite[GamePlay.Instance.myHandCardIndex[i]];
        //            //cards[i].sprite = cardSprite[1];
        //        }
        //    }
        //    stepCount++;            
        //}

        public void MyTurnAnimation()
        {
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
            float angle = currentValue / 100 * 360;
            sparkMark.localEulerAngles = new Vector3(0, 0, -angle);
        }

        private Vector2 CardDistributionAnimationPos(Vector3 dealerPos, Vector3 playerPos, int playerNum, int stepNum, int stepCount)
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
                    Vector2 newPos = new Vector2(dealerPosX + offsetValue / 6 * 5 - deltaPosX * stepCount, dealerPosY - offsetValue / 3 - deltaPOsY * stepCount);
                    result = newPos;
                }
                else
                {
                    Vector2 newPos = new Vector2(dealerPosX + offsetValue / 6 * 5 - deltaPosX * stepCount + 20, dealerPosY - offsetValue / 3 - deltaPOsY * stepCount);
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
        IEnumerator MoveCards(GameObject[] cards, GameObject originObject, GameObject[] targets, float overTime)
        {
            float startTime;
            Vector3 vectorOffset = new Vector3(-0.07f, 0, 0.01f);
            Vector3 supportOffset = new Vector3(0.6f, 0.1f, 0);
            //animate each flop card to its target over time "overTime"
            for (var i = 0; i < 10; i++)
            {
                Image[] cardImage = targets[i].GetComponentsInChildren<Image>();

                startTime = Time.time;
                if (i < 1)
                {
                    //translate the cards
                    while (Time.time < startTime + overTime)
                    {

                        cards[i * 2].transform.position = Vector3.Lerp(originObject.GetComponentInChildren<Image>().transform.position, cardImage[3].transform.position, (Time.time - startTime) / overTime);
                        cards[i * 2 + 1].transform.position = Vector3.Lerp(originObject.GetComponentInChildren<Image>().transform.position, cardImage[3].transform.position + vectorOffset, (Time.time - startTime) / overTime);

                        yield return null;

                    }

                    cards[i * 2].transform.position = cardImage[3].transform.position + supportOffset;
                    cards[i * 2 + 1].transform.position = cardImage[3].transform.position + vectorOffset * 4 + supportOffset;
                    
                    cards[i * 2].transform.localScale = new Vector3(35, 35, 1);
                    cards[i * 2 + 1].transform.localScale = new Vector3(35, 35, 1);


                }
                else
                {
                    while (Time.time < startTime + overTime)
                    {

                        cards[i * 2].transform.position = Vector3.Lerp(originObject.GetComponentInChildren<Image>().transform.position, cardImage[3].transform.position, (Time.time - startTime) / overTime);
                        cards[i * 2 + 1].transform.position = Vector3.Lerp(originObject.GetComponentInChildren<Image>().transform.position, cardImage[3].transform.position + vectorOffset, (Time.time - startTime) / overTime);

                        yield return null;

                    }

                    cards[i * 2].transform.position = cardImage[3].transform.position;
                    cards[i * 2 + 1].transform.position = cardImage[3].transform.position + vectorOffset;
                    cards[i * 2].transform.Rotate(Vector3.forward * -5, Space.World);
                    cards[i * 2 + 1].transform.Rotate(Vector3.forward * 15, Space.World);

                   
                }
                
            }
            for (int i = 0; i < cards.Length / 2; i++)
            {
                 startTime = Time.time;
                if (i < 2)
                {
                    while (Time.time < startTime + overTime *3 )
                    {
                        cards[i].transform.Rotate(Vector3.up * 7, Space.World);
                        //cards[i + 1].transform.Rotate(Vector3.up * 10, Space.World);
                        
                        if (cards[i].transform.rotation.eulerAngles.z >= -90f)
                        {
                            cards[i].GetComponent<SpriteRenderer>().sprite = cardSprite[0];

                        }

                        yield return null;

                    }
                    cards[i].transform.rotation = Quaternion.EulerRotation(new Vector3(0, 0, 0));
                }
            }
        }

        IEnumerator RotateCard(GameObject[] Cards, float overTime)
        {
            for (int i = 0; i < cards.Length/2; i++)
            {

                float startTime = Time.time;
                if (i < 1)
                {
                    while (Time.time < startTime + overTime)
                    {
                        cards[i].transform.Rotate(Vector3.up * 6, Space.World);
                        cards[i + 1].transform.Rotate(Vector3.up * 6, Space.World);
                        
                        yield return null;

                    }
                }
            }
        }
        IEnumerator DrawCardsAni(GameObject[] drawcards , float cardDistance ,Vector3 originPos, float overTime ,int cardIndex)
        {
            float startTime;

            //animate each flop card to its target over time "overTime"
            for (var i = 0; i < cardIndex; i++)
            {

                startTime = Time.time;
                if (i < 1)
                {
                    while (Time.time < startTime + overTime)
                    {

                        drawCards[i].transform.position = Vector3.Lerp(drawCards[i].transform.position, originPos, (Time.time - startTime) / overTime);
                        
                        yield return null;

                    }
                }
                else
                {
                    while (Time.time < startTime + overTime)
                    {
                        Vector3 newPos = new Vector3(drawCards[i].transform.position.x + cardDistance, drawCards[i].transform.position.y, drawCards[i].transform.position.z);
                        drawCards[i].transform.position = Vector3.Lerp(drawCards[i-1].transform.position, newPos, (Time.time - startTime) / overTime);

                        yield return null;

                    }
                    drawCards[i].transform.position = drawCards[i].transform.position;
                }
                
            }

            for (var i = 0; i < cardIndex; i++)
            {
                startTime = Time.time;
                while (Time.time < startTime + overTime)
                {
                    drawcards[i].transform.Rotate(Vector3.up * 5, Space.World);
                    if (drawcards[i].transform.rotation.eulerAngles.z >= -90f)
                    {

                        //CHANGE THE MATERIAL
                        drawcards[i].GetComponent<SpriteRenderer>().sprite = cardSprite[1];
                    }
                    yield return null;

                }






                drawcards[i].transform.rotation = Quaternion.EulerRotation(new Vector3(0, 0, 0));

            }


        }

        IEnumerator PotsAni(GameObject[] potObject, Vector3 targetPos, float overTime)
        {
            float startTime;

            //animate each flop card to its target over time "overTime"
            

                startTime = Time.time;
            for (int i = 0; i <10; i++)
            {
                while (Time.time < startTime + overTime)
                {
                   
                        potObject[i].transform.position = Vector3.Lerp(potObject[i].transform.position, targetPos, (Time.time - startTime) / overTime/2);
                    yield return null;
                    }
                potObject[i].transform.position = targetPos;
            }
            
        }
        private void PotArrangment()
        {
            for (int i = 0; i < pots.Length; i++)
            {
                pots[i].transform.position = players[i].transform.position;
            }
        }
    }
}
  
