using Unity.VisualScripting;
using UnityEngine.AI;
using UnityEngine;

namespace States
{
    internal class SeekState : State
    {
        private Transform _target;
        
        internal override void Execute()
        {
            if (StateMachine.Agent.remainingDistance == 0)
            {
                StateMachine.HasTarget = false;
                StateMachine.CurrentState = new IdleState(StateMachine);
            }
            else if (StateMachine.Target != _target)
            {
                StateMachine.CurrentState = new SeekState(StateMachine, StateMachine.Target);
            }
        }

        public SeekState(StateMachine stateMachine, Transform target) : base(stateMachine)
        {
            StateMachine = stateMachine;
            _target = target;
            StateMachine.Agent.destination = _target.position;
        }
    }
}