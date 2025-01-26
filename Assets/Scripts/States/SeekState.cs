using Items;

namespace States
{
    /// <summary>
    /// State dog is in when pathfinding to an item
    /// </summary>
    public class SeekState : State
    {
        private Stimulus _stimulus;
        
        public override void Execute()
        {
            if (StateMachine.Agent.remainingDistance == 0 || _stimulus.Inspected())
            {
                // when the item is reached, transitions to the inspecting state
                StateMachine.Queue.Dequeue();
                StateMachine.CurrentState = new InspectState(StateMachine, _stimulus);
            }
            else if (StateMachine.Queue.Peek() != _stimulus)
            {
                // if another, higher priority stimulus is encountered, swaps to that one
                _stimulus.Focused(false);
                StateMachine.CurrentState = new SeekState(StateMachine, StateMachine.Queue.Peek());
            }
        }

        public SeekState(StateMachine stateMachine, Stimulus stimulus) : base(stateMachine)
        {
            StateMachine = stateMachine;
            
            // doesn't dequeue the stimulus from the queue until it's finished pathfinding
            // allows dog to 'change its mind' and switch to another stimulus before returning to this one
            _stimulus = stimulus;
            _stimulus.Focused(true);
            
            // pathfinding to the item that holds the stimulus
            StateMachine.Agent.destination = _stimulus.transform.parent.position;
        }
    }
}