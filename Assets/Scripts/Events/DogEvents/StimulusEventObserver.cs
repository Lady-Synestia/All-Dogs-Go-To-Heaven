using System;
using UnityEngine;

namespace Events.DogEvents
{
    public class StimulusObserver : DogEventObserver
    {
        private void OnTriggerEnter(Collider other)
        {
            Stimulus stimulus = other.GetComponent<Stimulus>();
            RaiseEvent(new StimulusEvent(stimulus, DogEvent.Type.Stimulus));
        }
    }
}