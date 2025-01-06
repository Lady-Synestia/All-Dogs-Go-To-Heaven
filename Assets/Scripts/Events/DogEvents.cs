using System;

namespace Events
{
    public class DogEvent : GameEvent
    {
        public enum Type
        {
            Move,
        }

        public DogEvent(Type e) : base(e) { }
    }

    public class DogEventObserver : IEventObserver<DogEvent>
    {
        public event EventHandler<DogEvent> OnEvent;

        public void RaiseEvent(DogEvent e)
        {
            OnEvent?.Invoke(this, e);
        }
    }
}