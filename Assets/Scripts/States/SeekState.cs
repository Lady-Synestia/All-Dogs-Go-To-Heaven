namespace States
{
    public class SeekState : State
    {
        private Stimulus _stimulus;
        
        public override void Execute()
        {
            if (StateMachine.Agent.remainingDistance == 0 || _stimulus.Inspected())
            {
                StateMachine.Queue.Dequeue();
                StateMachine.CurrentState = new InspectState(StateMachine, _stimulus);
            }
            else if (StateMachine.Queue.Peek() != _stimulus)
            {
                _stimulus.Focused(false);
                StateMachine.CurrentState = new SeekState(StateMachine);
            }
        }

        public SeekState(StateMachine stateMachine) : base(stateMachine)
        {
            StateMachine = stateMachine;
            _stimulus = StateMachine.Queue.Peek();
            _stimulus.Focused(true);
            StateMachine.Agent.destination = _stimulus.transform.parent.position;
        }
    }
}