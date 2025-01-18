using System;

namespace Events
{
    public abstract class GameEvent
    {
        public Enum EventType { get; private set; }
        
        public EventArgs Args { get; private set; }

        protected GameEvent(Enum eventType, EventArgs args)
        {
            EventType = eventType;
            Args = args;
        }
    }
}
