using UnityEngine;
using System;

namespace Events.DogEvents
{
    public class DogEventObserver : EventObserver<DogEvent>
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Stimulus"))
            {
                StimulusEventArgs args = new ()
                {
                    Stimulus = other.GetComponent<Stimulus>()
                };
            
                RaiseEvent(new DogEvent(DogEvent.Type.Stimulus, args));
            }
        }
    }
}