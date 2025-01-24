using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI
{
    internal static class ButtonActions
    {
        public static void Quit()
        {
            Application.Quit();
        }

        public static void ChangeUI(string from, string to)
        {
            VisualElement toDocument =  GameObject.Find(to).GetComponent<UIDocument>().rootVisualElement;
            VisualElement fromDocument =  GameObject.Find(from).GetComponent<UIDocument>().rootVisualElement;

            toDocument.visible = true;
            fromDocument.visible = false;
        }

        public static void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public static void Pause()
        {
            Time.timeScale = 0;
            ChangeUI("HUD", "Pause");
        }
        
        public static void Resume()
        {
            Time.timeScale = 1;
            ChangeUI("Pause", "HUD");
        }
    }
}
