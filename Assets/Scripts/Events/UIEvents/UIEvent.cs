using System;

namespace Events.UIEvents
{
    public class UIEvent : GameEvent
    {
        public enum Type
        {
            ChangeUI,
            ChangeScene,
            Pause,
            Resume,
            ResetData,
            Quit
        }
        
        public UIEvent(Type type, EventArgs args) : base(type, args) { }
    }

    public class UIEventArgs : EventArgs
    {
        public string SceneTarget { get; set; } = "";
        public string UIFrom { get; set; } = "";
        public string UITo { get; set; } = "";
    }
}