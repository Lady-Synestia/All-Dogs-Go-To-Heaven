using System;
using Events.UIEvents;

namespace UI
{
    /// <summary>
    /// Class for the MainMenu UIElement
    /// </summary>
    public class MainMenu : UIElement
    {
        protected override void PostLoad()
        {
            RegisterButton("Quit", UIEvent.Type.Quit, EventArgs.Empty);
            RegisterButton("Play", UIEvent.Type.ChangeUI, new UIEventArgs{ UIFrom = "Main Menu", UITo = "Game Settings" });
            RegisterButton("Controls", UIEvent.Type.ChangeUI, new UIEventArgs{ UIFrom = "Main Menu", UITo = "Controls" });

            // set as visible when the scene loads
            Root.visible = true;
        }
    }
}


