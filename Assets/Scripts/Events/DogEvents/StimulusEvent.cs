using System;
using UnityEngine;

namespace Events.DogEvents
{
    public class StimulusEvent : DogEvent
    {
        public Stimulus Stimulus;
        
        public StimulusEvent(Stimulus initiator, Type e) : base(e) { Stimulus = initiator; }
    }
}