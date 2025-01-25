using System;
using UnityEngine;
using UnityEngine.UIElements;
using Events.UIEvents;

namespace UI
{
    public class UIElement : MonoBehaviour
    {
        protected VisualElement Root;
        
        protected void RegisterButton(string label, UIEvent.Type type, EventArgs args)
        {
            Button button = Root.Q<Button>(label);
            button.RegisterCallback<MouseUpEvent>(evt => UIEventObserver.Instance.RaiseEvent(new UIEvent(type, args)));
        }

        private void Awake()
        {
            Root = GetComponent<UIDocument>().rootVisualElement;
            UIEventObserver.Instance.OnEvent += OnButtonPress;
        }

        protected void OnButtonPress(object sender, UIEvent e)
        {
            switch (e.EventType)
            {
                case UIEvent.Type.ChangeUI:
                    UIEventArgs uiArgs = (UIEventArgs)e.Args;
                    if (uiArgs.UIFrom == name)
                    {
                        Root.visible = false;
                    }
                    else if (uiArgs.UITo == name)
                    {
                        Root.visible = true;
                    }
                    break;
            }
        }
    }
}