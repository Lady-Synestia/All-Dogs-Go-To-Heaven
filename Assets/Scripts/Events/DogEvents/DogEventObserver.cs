using UnityEngine;
using System;
using Items;

namespace Events.DogEvents
{
    /// <summary>
    /// Observer for the Dog Events.
    /// Will raise a new Stimulus event when the Dog encounters a stimulus
    /// </summary>
    public class DogEventObserver : EventObserver<DogEvent>
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Stimulus"))
            {
                // getting a reference to the stimulus object encountered
                StimulusEventArgs args = new ()
                {
                    Stimulus = other.GetComponent<Stimulus>()
                };
            
                RaiseEvent(new DogEvent(DogEvent.Type.Stimulus, args));
            }
        }
    }
}