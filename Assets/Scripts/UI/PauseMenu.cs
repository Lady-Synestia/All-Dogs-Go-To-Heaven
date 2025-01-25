using System;
using Events.UIEvents;

namespace UI
{
    public class PauseMenu : UIElement
    {
        private void Start()
        {
            RegisterButton("Quit", UIEvent.Type.Quit, EventArgs.Empty);
            RegisterButton("Resume", UIEvent.Type.Resume, EventArgs.Empty);
            RegisterButton("Restart", UIEvent.Type.ChangeScene, new UIEventArgs{SceneTarget = "Level"});
            RegisterButton("MainMenu", UIEvent.Type.ChangeScene, new UIEventArgs{SceneTarget = "Main Menu"});
            
            Root.visible = false;
            UIEventObserver.Instance.OnEvent += ButtonPressed;
        }

        private void ButtonPressed(object sender, UIEvent e)
        {
            Root.visible = e.EventType switch
            {
                UIEvent.Type.Pause => true,
                UIEvent.Type.Resume => false,
                _ => Root.visible
            };
        }
    }
}
