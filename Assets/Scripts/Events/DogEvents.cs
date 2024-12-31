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

        public void RaiseEvent()
        {
            OnEvent?.Invoke(this, new DogEvent(DogEvent.Type.Move));
        }
    }
}