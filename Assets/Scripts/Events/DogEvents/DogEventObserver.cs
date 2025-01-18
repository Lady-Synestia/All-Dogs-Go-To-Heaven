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
    }
}