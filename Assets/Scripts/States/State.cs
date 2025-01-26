namespace States
{
    public abstract class State 
    {
        protected StateMachine StateMachine;
        
        public abstract void Execute();
        
        protected State(StateMachine stateMachine) { StateMachine = stateMachine; }
    }
}
