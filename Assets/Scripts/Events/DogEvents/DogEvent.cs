using System;
using UnityEngine;

namespace Events.DogEvents
{
    public class DogEvent : GameEvent
    {
        public enum Type
        {
            Stimulus,
            SceneReady,
        }

        public DogEvent(Type type, EventArgs args) : base(type, args) { }
    }
    
    public class StimulusEventArgs : EventArgs
    {
        public Stimulus Stimulus { get; set; }
    }
}