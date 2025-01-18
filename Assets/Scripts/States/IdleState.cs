using UnityEngine;

namespace States
{
    internal class IdleState : State
    {
        
        internal override void Execute()
        {
            if (StateMachine.HasTarget)
            {
                StateMachine.CurrentState = new SeekState(StateMachine, StateMachine.Target);
            }
        }

        public IdleState(StateMachine stateMachine) : base(stateMachine) { }
    }
}