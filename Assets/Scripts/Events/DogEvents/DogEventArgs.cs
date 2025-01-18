using System;

namespace Events.DogEvents
{
    public class StimulusEventArgs : EventArgs
    {
        public Stimulus Stimulus { get; set; }
    }
}