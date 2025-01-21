namespace States
{
    internal class IdleState : State
    {
        
        internal override void Execute()
        {
            if (!StateMachine.Queue.IsEmpty())
            {
                StateMachine.CurrentState = new SeekState(StateMachine);
            }
        }

        public IdleState(StateMachine stateMachine) : base(stateMachine) { }
    }
}