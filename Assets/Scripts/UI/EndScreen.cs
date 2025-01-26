using System;
using Events.UIEvents;

namespace UI
{
    public class EndScreen : UIElement
    {
        protected override void PostLoad()
        {
            RegisterButton("Replay", UIEvent.Type.ChangeScene, new UIEventArgs{SceneTarget = "Level"});
            RegisterButton("MainMenu", UIEvent.Type.ChangeScene, new UIEventArgs{SceneTarget = "Main Menu"});
        }

        protected override void UIEventRaised(object sender, UIEvent e)
        {
            Root.visible = e.EventType switch
            {
                UIEvent.Type.GameEnd => true,
                _ => false,
            };
        }
    }
}