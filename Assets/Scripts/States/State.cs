namespace States
{
    /// <summary>
    /// Base Class for states. Provides common execution and initialisation
    /// </summary>
    public abstract class State 
    {
        protected StateMachine StateMachine;
        
        // derived classes must implement Execute
        public abstract void Execute();
        
        protected State(StateMachine stateMachine) { StateMachine = stateMachine; }
    }
}
