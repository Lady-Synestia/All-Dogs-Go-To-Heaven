using System;

namespace Events
{
    /// <summary>
    /// Base Class for Game Events to inherit from. Provides common fields for Event Type and Arguments
    /// </summary>
    public abstract class GameEvent
    {
        // possible types are defined by derived classes
        public Enum EventType { get; private set; }
        
        // custom Argument classes are defined for some event types
        public EventArgs Args { get; private set; }
        
        protected GameEvent(Enum eventType, EventArgs args)
        {
            EventType = eventType;
            Args = args;
        }
    }
}
