using System;
using Events.UIEvents;

namespace UI
{
    /// <summary>
    /// Class for the HUD UI Document
    /// </summary>
    public class HUD : UIElement
    {
        protected override void PostLoad()
        {
            RegisterButton("Pause", UIEvent.Type.Pause, EventArgs.Empty);
            
            // set as visible when the scene loads
            Root.visible = true;
        }

        protected override void UIEventRaised(object sender, UIEvent e)
        {
            // visibility is toggled when the game is paused/unpaused
            Root.visible = e.EventType switch
            {
                UIEvent.Type.Pause => false,
                UIEvent.Type.Resume => true,
                _ => Root.visible
            };
        }
    }
}
