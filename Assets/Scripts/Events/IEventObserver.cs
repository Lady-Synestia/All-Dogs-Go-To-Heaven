using System;

namespace Events
{
    internal interface IEventObserver<T>
    {
        public event EventHandler<T> OnEvent;
        
        public void RaiseEvent(T e);
    }

}