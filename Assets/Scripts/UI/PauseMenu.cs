using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        private void OnEnable()
        {
            VisualElement container = GetComponent<UIDocument>().rootVisualElement;
            
            Button quitButton = container.Q<Button>("Quit");
            quitButton.RegisterCallback<MouseUpEvent>((evt) => ButtonActions.Quit());
            
            Button resumeButton = container.Q<Button>("Resume");
            resumeButton.RegisterCallback<MouseUpEvent>((evt) => ButtonActions.Resume());
            
            Button restartButton = container.Q<Button>("Restart");
            restartButton.RegisterCallback<MouseUpEvent>((evt) => ButtonActions.ChangeScene("Level"));
            
            Button menuButton = container.Q<Button>("MainMenu");
            menuButton.RegisterCallback<MouseUpEvent>((evt) => ButtonActions.ChangeScene("Main Menu"));

            container.visible = false;
        }
    }
}
