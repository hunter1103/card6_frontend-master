using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SceneScripts
{
    public class LoginScene : MonoBehaviour
    {
        public static LoginScene Instance;
        public GameObject loginScene;
        public GameObject SignupScene;
        public GameObject beforeLogin;
        // Use this for initialization

        public void LoginClick()
        {
            SceneManager.SwitchScene("MainBoardScene");

        }
        public void SignUpClick()
        {
            loginScene.SetActive(false);
            SignupScene.SetActive(true);
            beforeLogin.SetActive(false);

        }
        public void SingInClick()
        {
            SceneManager.SwitchScene("MainBoardScene");
        }
        public void ResisterClick()
        {
            loginScene.SetActive(true);
            SignupScene.SetActive(false);
            beforeLogin.SetActive(false);
        }
        public void GeustClick()
        {
            SceneManager.SwitchScene("MainBoardScene");
        }
      

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            loginScene.SetActive(false);
            SignupScene.SetActive(false);
            beforeLogin.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
