using System;
using UnityEngine;

namespace Events
{
    internal interface IEventObserver<T>
    {
        public event EventHandler<T> OnEvent;
        
        public void RaiseEvent(T e);
    }

    public class EventObserver<T> : MonoBehaviour, IEventObserver<T>
    {
        public event EventHandler<T> OnEvent;

        public void RaiseEvent(T e)
        {
            OnEvent?.Invoke(this, e);
        }
    }
}