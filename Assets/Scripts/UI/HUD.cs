using System;
using Events.UIEvents;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class HUD : UIElement
    {
        private void Start()
        {
            RegisterButton("Pause", UIEvent.Type.Pause, EventArgs.Empty);

            UIEventObserver.Instance.OnEvent += ButtonPressed;
        }

        private void ButtonPressed(object sender, UIEvent e)
        {
            Root.visible = e.EventType switch
            {
                UIEvent.Type.Pause => false,
                UIEvent.Type.Resume => true,
                _ => Root.visible
            };
        }
    }
}


