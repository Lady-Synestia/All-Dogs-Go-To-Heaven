using UnityEngine;
using System;

namespace Events.PlayerEvents
{
    public class MovementEvent : PlayerEvent
    {
        public Vector3 Value { get; private set; }

        public MovementEvent(Vector3 value, Type e) : base(e) { Value = value; }
    }
}