using System;
using UnityEngine;
using UnityEngine.UIElements;
using Events.UIEvents;

namespace UI
{
    /// <summary>
    /// Base Class for UI Elements.
    /// Provides common functionality for loading, registering buttons and responding to events.
    /// </summary>
    public abstract class UIElement : MonoBehaviour
    {
        protected VisualElement Root;
        public void Load()
        {
            // UI document is hidden by default
            Root = GetComponent<UIDocument>().rootVisualElement;
            Root.visible = false;
            UIEventObserver.Instance.Subscribe(OnUIEvent);
            
            PostLoad();
        }
        /// <summary>
        /// Must be defined by derived classes, called after the base class loads
        /// </summary>
        protected abstract void PostLoad();
        
        /// <summary>
        /// Registers a UIEvent to the MouseUpEvent of a specified button
        /// </summary>
        /// <param name="label">The name of the button</param>
        /// <param name="type">Type of UIEvent to call</param>
        /// <param name="args">Event arguments to include</param>
        protected void RegisterButton(string label, UIEvent.Type type, EventArgs args)
        {
            Button button = Root.Q<Button>(label);
            button.RegisterCallback<MouseUpEvent>(evt => UIEventObserver.Instance.RaiseEvent(new UIEvent(type, args)));
        }
        
        protected void OnUIEvent(object sender, UIEvent e)
        {
            // function is subscribed to the OnEvent handler of the UIEventObserver
            switch (e.EventType)
            {
                case UIEvent.Type.ChangeUI:
                    // ChangeUI response is common for all UI elements
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
                    // all elements default to false when the game ends
                    Root.visible = false;
                    break;
            }
            UIEventRaised(sender, e);
        }
        /// <summary>
        /// Can be defined by derived classes if they have unique responses to events.
        /// </summary>
        protected virtual void UIEventRaised(object sender, UIEvent e) {}
    }
}