
using UnityEngine;
namespace Assets.Scripts.SceneScripts
{
    public class SceneManager : MonoBehaviour
    {
        
        public static void SwitchScene(string newScene)
        {
            if (Application.loadedLevelName != newScene)
            {
                Application.LoadLevel(newScene);
            }
        }
        public static void SwtichToLoginScene()
        {
            SwitchScene("LoginScene");
        }
    }
}
