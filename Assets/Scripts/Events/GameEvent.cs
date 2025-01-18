using System;

namespace Events
{

    internal interface IEventObserver<T>
    {
        public event EventHandler<T> OnEvent;
        
        public void RaiseEvent(T e);
    }

    public abstract class GameEvent
    {
        public Enum EventType { get; private set; }

        protected GameEvent(Enum customEvent)
        {
            EventType = customEvent;
        }
    }
}
