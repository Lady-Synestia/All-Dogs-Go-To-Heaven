using System;
using Items;
using UnityEngine;

namespace Events.DogEvents
{
    /// <summary>
    /// Defines the Types of Dog Events that can occur.
    /// </summary>
    public class DogEvent : GameEvent
    {
        public enum Type
        {
            Stimulus,
            ItemInspected
        }
        
        public DogEvent(Type type, EventArgs args) : base(type, args) { }
    }
    
    /// <summary>
    /// Provides arguments for the "Stimulus" Dog Event 
    /// </summary>
    public class StimulusEventArgs : EventArgs
    {
        /// <summary>
        /// Stimulus object that the Dog encountered
        /// </summary>
        public Stimulus Stimulus { get; set; }
    }
}