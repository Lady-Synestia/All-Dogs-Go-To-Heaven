using System;
using UnityEngine;

namespace Events.DogEvents
{
    public class DogEvent : GameEvent
    {
        public enum Type
        {
            Stimulus,
        }

        public DogEvent(Type type, EventArgs args) : base(type, args) { }
    }
}