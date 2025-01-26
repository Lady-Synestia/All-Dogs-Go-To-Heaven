using System;
using UnityEngine;
using UnityEngine.UIElements;
using Events.UIEvents;

namespace UI
{
    public abstract class UIElement : MonoBehaviour
    {
        protected VisualElement Root;
        public void Load()
        {
            Root = GetComponent<UIDocument>().rootVisualElement;
            Root.visible = false;
            UIEventObserver.Instance.Subscribe(OnUIEvent);
            PostLoad();
        }
        
        protected void RegisterButton(string label, UIEvent.Type type, EventArgs args)
        {
            Button button = Root.Q<Button>(label);
            button.RegisterCallback<MouseUpEvent>(evt => UIEventObserver.Instance.RaiseEvent(new UIEvent(type, args)));
        }
        
        protected abstract void PostLoad();
        
        protected void OnUIEvent(object sender, UIEvent e)
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
                case UIEvent.Type.GameEnd:
                    Root.visible = false;
                    break;
            }
            UIEventRaised(sender, e);
        }
        
        protected virtual void UIEventRaised(object sender, UIEvent e) {}
    }
}