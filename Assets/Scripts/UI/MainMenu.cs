using System;
using Events.UIEvents;

namespace UI
{
    public class MainMenu : UIElement
    {
        private void Start()
        {
            RegisterButton("Quit", UIEvent.Type.Quit, EventArgs.Empty);
            RegisterButton("Play", UIEvent.Type.ChangeUI, new UIEventArgs{ UIFrom = "MainMenu", UITo = "GameSettings" });
        }
    }
}


