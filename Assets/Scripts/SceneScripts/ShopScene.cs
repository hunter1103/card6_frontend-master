using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SceneScripts
{
    public class ShopScene : MonoBehaviour
    {
        public void ReturnClick()
        {
            SceneManager.SwitchScene("MainBoardScene");
            Debug.Log("werwere");
        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
