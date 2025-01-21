using UnityEngine;

namespace States
{
    internal class InspectState : State
    {
        private const float Timer = 1.0f;
        private float _startTime;
        private Stimulus _stimulus;
        
        internal override void Execute()
        {
            float timeElapsed = Time.time - _startTime;
            if (timeElapsed >= Timer)
            {
                _stimulus.Inspect();
                StateMachine.CurrentState = new IdleState(StateMachine);
            }
        }

        public InspectState(StateMachine stateMachine, Stimulus stimulus) : base(stateMachine)
        {
            _stimulus = stimulus;
            _startTime = Time.time;
        }
    }
}