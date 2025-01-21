using UnityEngine;
using System;

namespace Events.DogEvents
{
    public class DogEventObserver : MonoBehaviour, IEventObserver<DogEvent>
    {
        public event EventHandler<DogEvent> OnEvent;

        public void RaiseEvent(DogEvent e)
        {
            OnEvent?.Invoke(this, e);
        }
        
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