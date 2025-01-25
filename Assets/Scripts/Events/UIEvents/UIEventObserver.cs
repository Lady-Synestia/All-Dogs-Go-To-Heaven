using System;
using System.ComponentModel;

namespace Events.UIEvents
{
    public class UIEventObserver : EventObserver<UIEvent>
    {
        public static UIEventObserver Instance { get; private set;}

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
    }
}