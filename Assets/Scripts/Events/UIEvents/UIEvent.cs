using System;

namespace Events.UIEvents
{
    /// <summary>
    /// Defines the Types of UI Event. Raised by the UIEventObserver
    /// </summary>
    public class UIEvent : GameEvent
    {
        public enum Type
        {
            ChangeUI,
            ChangeScene,
            Pause,
            Resume,
            ResetData,
            Quit,
            GameEnd
        }
        
        public UIEvent(Type type, EventArgs args) : base(type, args) { }
    }

    /// <summary>
    /// Arguments for some of the UIEvents.
    /// </summary>
    public class UIEventArgs : EventArgs
    {
        /// <summary>
        /// Required for a ChangeScene Event. Represents the name of the scene to change to
        /// </summary>
        public string SceneTarget { get; set; } = "";
        
        /// <summary>
        /// Required for a ChangeUI event. Represents the name of the UI to hide
        /// </summary>
        public string UIFrom { get; set; } = "";
        
        /// <summary>
        /// Required for a ChangeUI event. Represents the name of the UI to show
        /// </summary>
        public string UITo { get; set; } = "";
    }
}