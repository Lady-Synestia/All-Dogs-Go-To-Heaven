using System;
using UnityEngine;
using UnityEngine.UIElements;
using Events.UIEvents;

namespace UI
{
    public class GameSettings : UIElement
    {
        private void Start()
        {
            RegisterButton("Back", UIEvent.Type.ChangeUI, new UIEventArgs{ UIFrom = "GameSettings", UITo = "MainMenu" });
            RegisterButton("Play", UIEvent.Type.ChangeScene, new UIEventArgs{ SceneTarget = "Level"});
            RegisterButton("Reset", UIEvent.Type.ResetData, EventArgs.Empty);

            Root.visible = false;
        }
    }
}


