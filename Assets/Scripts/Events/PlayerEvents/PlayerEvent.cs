using UnityEngine;

namespace Events.PlayerEvents
{
    public class PlayerEvent : GameEvent
    {
        public enum Type
        {
            Move,
            Look
        }

        public PlayerEvent(Type e) : base(e) { }
    }

    
}