using System;
using UnityEngine;

namespace Events
{
    /// <summary>
    /// Base Class for the Event Observers. Provides Definitions for the common event functionality
    /// </summary>
    /// <typeparam name="T">The Type of Event to Raise</typeparam>
    public abstract class EventObserver<T> : MonoBehaviour where T : GameEvent
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