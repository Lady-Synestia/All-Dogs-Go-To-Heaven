using System;
using Events.UIEvents;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Class for the GameSettings UI Document
    /// </summary>
    public class GameSettings : UIElement
    {
        protected override void PostLoad()
        {
            RegisterButton("Back", UIEvent.Type.ChangeUI, new UIEventArgs{ UIFrom = "Game Settings", UITo = "Main Menu" });
            RegisterButton("Play", UIEvent.Type.ChangeScene, new UIEventArgs{ SceneTarget = "Level"});
            RegisterButton("Reset", UIEvent.Type.ResetData, EventArgs.Empty);
        }
    }
}
