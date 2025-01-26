namespace Events.UIEvents
{
    /// <summary>
    /// Singleton event observer for UI events. Instantiated by the Game Manager
    /// </summary>
    public class UIEventObserver : EventObserver<UIEvent>
    {
        public static UIEventObserver Instance { get; private set;}
        
        // enforces singleton access by preventing new objects from being created.
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