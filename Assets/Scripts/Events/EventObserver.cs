using System;
using UnityEngine;

namespace Events
{
    public abstract class EventObserver<T> : MonoBehaviour
    {
        public event EventHandler<T> OnEvent;

        public void RaiseEvent(T e)
        {
            OnEvent?.Invoke(this, e);
        }

        public void Subscribe(EventHandler<T> handler)
        {
            OnEvent += handler;
        }
    }
}