using System;
using UnityEngine;

namespace Events.PlayerEvents
{
    /// <summary>
    /// Defines the types of Player Events.
    /// </summary>
    public class PlayerEvent : GameEvent
    {
        public enum Type
        {
            Move,
            Look
        }

        public PlayerEvent(Type type, EventArgs args) : base(type, args) { }
    }
    
    /// <summary>
    /// Holds the arguments for a Player Movement Event
    /// </summary>
    public class MovementEventArgs : EventArgs
    {
        /// <summary>
        /// Movement input value - can store either movement input or look rotation values
        /// </summary>
        public Vector3 Value { get; set; }
    }
}