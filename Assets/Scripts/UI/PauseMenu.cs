using System;
using Events.UIEvents;

namespace UI
{
    /// <summary>
    /// Class for the PauseMenu UI Document
    /// </summary>
    public class PauseMenu : UIElement
    {
        protected override void PostLoad()
        {
            RegisterButton("Quit", UIEvent.Type.Quit, EventArgs.Empty);
            RegisterButton("Resume", UIEvent.Type.Resume, EventArgs.Empty);
            RegisterButton("Restart", UIEvent.Type.ChangeScene, new UIEventArgs{SceneTarget = "Level"});
            RegisterButton("MainMenu", UIEvent.Type.ChangeScene, new UIEventArgs{SceneTarget = "Main Menu"});
        }

        protected override void UIEventRaised(object sender, UIEvent e)
        {
            // visibility is toggled when the game is paused/resumed
            Root.visible = e.EventType switch
            {
                UIEvent.Type.Pause => true,
                UIEvent.Type.Resume => false,
                _ => Root.visible
            };
        }
    }
}
