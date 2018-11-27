using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SceneScripts
{
    public class MainBoardScene : MonoBehaviour
    {


        // Use this for initialization
        public void ReturnClick()
        {
            SceneManager.SwitchScene("LoginScene");
        }
        public void ShopClick()
        {
            SceneManager.SwitchScene("ShopScene");
        }
        public void SettingClick()
        {
            SceneManager.SwitchScene("SettingScene");
        }
        public void HelpClick()
        {
            SceneManager.SwitchScene("HelpScene");

        }
        public void ProfileClick()
        {
            SceneManager.SwitchScene("ProfileScene");
        }
        public void MainBoardClick()
        {
            SceneManager.SwitchScene("MainBoardScene");
        }
        public void PlayRoomClick()
        {
            SceneManager.SwitchScene("RoomScene");
        }
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
