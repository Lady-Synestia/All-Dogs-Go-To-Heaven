using System;
using Events.UIEvents;

namespace UI
{
    /// <summary>
    /// Class for the EndScreen UI Document
    /// </summary>
    public class EndScreen : UIElement
    {
        protected override void PostLoad()
        {
            RegisterButton("Replay", UIEvent.Type.ChangeScene, new UIEventArgs{SceneTarget = "Level"});
            RegisterButton("MainMenu", UIEvent.Type.ChangeScene, new UIEventArgs{SceneTarget = "Main Menu"});
        }
        
        protected override void UIEventRaised(object sender, UIEvent e)
        {
            // the end screen is shown when the GameEnd event is raised
            Root.visible = e.EventType switch
            {
                UIEvent.Type.GameEnd => true,
                _ => false,
            };
        }
    }
}