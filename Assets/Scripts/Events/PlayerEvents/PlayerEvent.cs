using System;
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

        public PlayerEvent(Type type, EventArgs args) : base(type, args) { }
    }
    
    public class MovementEventArgs : EventArgs
    {
        public Vector3 Value { get; set; }
    }
}