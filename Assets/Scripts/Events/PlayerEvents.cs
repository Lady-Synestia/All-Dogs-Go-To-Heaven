using System;

namespace Events
{
    public class PlayerEvent : GameEvent
    {
        public enum Type
        {
            Move,
        }

        public PlayerEvent(Type e) : base(e) { }
    }

    public class PlayerEventObserver: IEventObserver<PlayerEvent>
    {
        public event EventHandler<PlayerEvent> OnEvent;

        public void RaiseEvent()
        {
            OnEvent?.Invoke(this, new PlayerEvent(PlayerEvent.Type.Move));
        }
    }
}