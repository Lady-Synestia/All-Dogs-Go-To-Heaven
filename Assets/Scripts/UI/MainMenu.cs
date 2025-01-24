using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        private void Awake()
        {
            VisualElement container = GetComponent<UIDocument>().rootVisualElement;
            
            Button quitButton = container.Q<Button>("Quit");
            quitButton.RegisterCallback<MouseUpEvent>((evt) => ButtonActions.Quit());
            
            Button playButton = container.Q<Button>("Play");
            playButton.RegisterCallback<MouseUpEvent>((evt) => ButtonActions.ChangeUI("MainMenu", "GameSettings"));
            
        }
    }
}


