using System;

namespace Events.PlayerEvents
{
    public class PlayerEvent : GameEvent
    {
        public enum Type
        {
            Move,
            Look
        }

        public PlayerEvent(Type type, EventArgs args) : base(type, args) { }
    }
}