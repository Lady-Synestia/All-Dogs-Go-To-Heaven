using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class HUD : MonoBehaviour
    {
        private void OnEnable()
        {
            VisualElement container = GetComponent<UIDocument>().rootVisualElement;
            
            Button pauseButton = container.Q<Button>("Pause");
            pauseButton.RegisterCallback<MouseUpEvent>((evt) => ButtonActions.Pause());
        }
    }
}


