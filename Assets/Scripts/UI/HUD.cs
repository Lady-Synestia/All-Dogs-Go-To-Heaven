using System;
using Events.UIEvents;

namespace UI
{
    public class HUD : UIElement
    {
        protected override void PostLoad()
        {
            RegisterButton("Pause", UIEvent.Type.Pause, EventArgs.Empty);
            Root.visible = true;
        }

        protected override void UIEventRaised(object sender, UIEvent e)
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
