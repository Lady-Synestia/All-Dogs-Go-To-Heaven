namespace States
{
    internal abstract class State 
    {
        protected StateMachine StateMachine;
        
        internal abstract void Execute();
        
        protected State(StateMachine stateMachine) { StateMachine = stateMachine; }
    }
}
