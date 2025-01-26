using Events.UIEvents;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class Controls : UIElement
    {
        protected override void PostLoad()
        {
            RegisterButton("Back", UIEvent.Type.ChangeUI, new UIEventArgs{UIFrom = "Controls", UITo = "Main Menu"});
        }
    }
}

