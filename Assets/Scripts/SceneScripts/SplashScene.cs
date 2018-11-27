using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts.SceneScripts
{
    public class SplashScene : MonoBehaviour
    {
        public GameObject loadingBar;
        public Slider slider;
        private int count =1;
        private int targetNum = 500;
        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (count < targetNum)
            {
                slider.value = (float)count / targetNum;
            }
            else if (count == targetNum)
            {
                SceneManager.SwitchScene("LoginScene");
            }
            count++;
        }
        
        
    }
}
