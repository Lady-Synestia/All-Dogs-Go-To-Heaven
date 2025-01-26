using System;
using Events.UIEvents;

namespace UI
{
    public class MainMenu : UIElement
    {
        protected override void PostLoad()
        {
            RegisterButton("Quit", UIEvent.Type.Quit, EventArgs.Empty);
            RegisterButton("Play", UIEvent.Type.ChangeUI, new UIEventArgs{ UIFrom = "Main Menu", UITo = "Game Settings" });
            RegisterButton("Controls", UIEvent.Type.ChangeUI, new UIEventArgs{ UIFrom = "Main Menu", UITo = "Controls" });

            Root.visible = true;
        }
    }
}


