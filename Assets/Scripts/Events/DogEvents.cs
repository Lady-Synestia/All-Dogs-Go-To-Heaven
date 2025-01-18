using System;
using UnityEngine;

namespace Events
{
    public class DogEvent : GameEvent
    {
        public enum Type
        {
            Stimulus,
        }

        public DogEvent(Type e) : base(e) { }
    }

    public class StimulusEvent : DogEvent
    {
        public Stimulus Stimulus;
        
        public StimulusEvent(Stimulus initiator, Type e) : base(e) { Stimulus = initiator; }
    }

    public class DogEventObserver : MonoBehaviour, IEventObserver<DogEvent>
    {
        public event EventHandler<DogEvent> OnEvent;
        
        public void RaiseEvent(DogEvent e)
        {
            OnEvent?.Invoke(this, e);
        }
    }

    public class StimulusObserver : DogEventObserver
    {
        private void OnTriggerEnter(Collider other)
        {
            Stimulus stimulus = other.GetComponent<Stimulus>();
            RaiseEvent(new StimulusEvent(stimulus, DogEvent.Type.Stimulus));
        }
    }
}