using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SceneScripts
{
    public class HelpScene : MonoBehaviour
    {
        public void ReturnClick()
        {
            SceneManager.SwitchScene("MainBoardScene");
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
